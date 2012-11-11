// ZipStorer, by Jaime Olivares
// Website: zipstorer.codeplex.com
// Version: 2.35 (March 14, 2010)

namespace NoteFly
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Text;

    /// <summary>
    /// Unique class for compression/decompression file. Represents a Zip file.
    /// </summary>
    public class ZipStorer : IDisposable
    {
        /// <summary>
        /// Represents an entry in Zip file directory
        /// </summary>
        public struct ZipFileEntry
        {
            /// <summary>Compression method</summary>
            public Compression Method; 

            /// <summary>Full path and filename as stored in Zip</summary>
            public string FilenameInZip;

            /// <summary>Original file size</summary>
            public uint FileSize;

            /// <summary>Compressed file size</summary>
            public uint CompressedSize;

            /// <summary>Offset of header information inside Zip storage</summary>
            public uint HeaderOffset;

            /// <summary>Offset of file inside Zip storage</summary>
            public uint FileOffset;

            /// <summary>Size of header information</summary>
            public uint HeaderSize;

            /// <summary>32-bit checksum of entire file</summary>
            public uint Crc32;

            /// <summary>Last modification time of file</summary>
            public DateTime ModifyTime;

            /// <summary>User comment for file</summary>
            public string Comment;

            /// <summary>True if UTF8 encoding for filename and comments, false if default (CP 437)</summary>
            public bool EncodeUTF8;

            /// <summary>Overriden method</summary>
            /// <returns>Filename in Zip</returns>
            public override string ToString()
            {
                return this.FilenameInZip;
            }
        }

        #region Public fields
        /// <summary>True if UTF8 encoding for filename and comments, false if default (CP 437)</summary>
        public bool EncodeUTF8 = false;

        /// <summary>Force deflate algotithm even if it inflates the stored file. Off by default.</summary>
        public bool ForceDeflating = false;
        #endregion

        #region Private fields
        /// <summary>
        /// List of files to store
        /// </summary>
        private List<ZipFileEntry> files = new List<ZipFileEntry>();

        /// <summary>
        /// Filename of storage file
        /// </summary>
        private string filename;

        /// <summary>
        /// Stream object of storage file
        /// </summary>
        private Stream zipfilestream;

        /// <summary>
        /// General comment
        /// </summary>
        private string comment = string.Empty;

        /// <summary>
        /// Central dir image
        /// </summary>
        private byte[] centraldirimage = null;

        /// <summary>
        /// Existing files in zip
        /// </summary>
        private ushort existingfiles = 0;

        /// <summary>
        /// File access for Open method
        /// </summary>
        private FileAccess fileaccess;

        /// <summary>
        /// Static CRC32 Table
        /// </summary>
        private static uint[] crctable = null;

        /// <summary>
        /// Compression method enumeration
        /// </summary>
        public enum Compression : ushort
        {
            /// <summary>Uncompressed storage</summary> 
            Store = 0,

            /// <summary>Deflate compression method</summary>
            Deflate = 8
        }

        /// <summary>
        /// Default filename encoder
        /// </summary>
        private static Encoding DefaultEncoding = Encoding.GetEncoding(437);
        #endregion

        #region Public methods
        /// <summary>
        /// Static constructor. 
        /// Just invoked once in order to create the CRC32 lookup table.
        /// </summary>
        static ZipStorer()
        {
            // Generate CRC32 table
            crctable = new uint[256];
            for (int i = 0; i < crctable.Length; i++)
            {
                uint c = (uint)i;
                for (int j = 0; j < 8; j++)
                {
                    if ((c & 1) != 0)
                    {
                        c = 3988292384 ^ (c >> 1);
                    }
                    else
                    {
                        c >>= 1;
                    }
                }

                crctable[i] = c;
            }
        }

        /// <summary>
        /// Method to create a new storage file
        /// </summary>
        /// <param name="zipfilename">Full path of Zip file to create</param>
        /// <param name="zipcomment">General comment for Zip file</param>
        /// <returns>A valid ZipStorer object</returns>
        public static ZipStorer Create(string zipfilename, string zipcomment)
        {
            Stream stream = new FileStream(zipfilename, FileMode.Create, FileAccess.ReadWrite);
            ZipStorer zip = Create(stream, zipcomment);
            zip.comment = zipcomment;
            zip.filename = zipfilename;
            return zip;
        }

        /// <summary>
        /// Method to create a new zip storage in a stream.
        /// </summary>
        /// <param name="stream">The zip file stream.</param>
        /// <param name="comment">The zip file comment.</param>
        /// <returns>A valid ZipStorer object</returns>
        public static ZipStorer Create(Stream zipstream, string zipcomment)
        {
            ZipStorer zip = new ZipStorer();
            zip.comment = zipcomment;
            zip.zipfilestream = zipstream;
            zip.fileaccess = FileAccess.Write;
            return zip;
        }

        /// <summary>
        /// Method to open an existing storage file.
        /// </summary>
        /// <param name="filename">Full path of Zip file to open</param>
        /// <param name="access">File access mode as used in FileStream constructor</param>
        /// <returns>A valid ZipStorer object</returns>
        public static ZipStorer Open(string zipfile, FileAccess zipfileaccess)
        {
            Stream stream = (Stream)new FileStream(zipfile, FileMode.Open, zipfileaccess == FileAccess.Read ? FileAccess.Read : FileAccess.ReadWrite);
            ZipStorer zip = Open(stream, zipfileaccess);
            zip.filename = zipfile;
            return zip;
        }

        /// <summary>
        /// Method to open an existing storage from stream
        /// </summary>
        /// <param name="stream">Already opened stream with zip contents</param>
        /// <param name="access">File access mode for stream operations</param>
        /// <returns>A valid ZipStorer object</returns>
        public static ZipStorer Open(Stream zipstream, FileAccess zipfileaccess)
        {
            if (!zipstream.CanSeek && zipfileaccess != FileAccess.Read)
            {
                throw new InvalidOperationException("Stream cannot seek");
            }

            ZipStorer zip = new ZipStorer();
            ////zip.FileName = _filename;
            zip.zipfilestream = zipstream;
            zip.fileaccess = zipfileaccess;
            if (zip.ReadFileInfo())
            {
                return zip;
            }

            throw new System.IO.InvalidDataException();
        }

        /// <summary>
        /// Add full contents of a file into the Zip storage
        /// </summary>
        /// <param name="compressionmethod">Compression method</param>
        /// <param name="pathname">Full path of file to add to Zip storage</param>
        /// <param name="filenameInZip">Filename and path as desired in Zip directory</param>
        /// <param name="zipfilecomment">Comment for stored file</param>
        public void AddFile(Compression compressionmethod, string pathname, string filenameInZip, string zipfilecomment)
        {
            if (this.fileaccess == FileAccess.Read)
                throw new InvalidOperationException("Writing is not alowed");

            FileStream stream = new FileStream(pathname, FileMode.Open, FileAccess.Read);
            this.AddStream(compressionmethod, filenameInZip, stream, File.GetLastWriteTime(pathname), zipfilecomment);
            stream.Close();
        }

        /// <summary>
        /// Add full contents of a stream into the Zip storage
        /// </summary>
        /// <param name="compressionmethod">Compression method</param>
        /// <param name="filenameInZip">Filename and path as desired in Zip directory</param>
        /// <param name="source">Stream object containing the data to store in Zip</param>
        /// <param name="modTime">Modification time of the data to store</param>
        /// <param name="comment">Comment for stored file</param>
        public void AddStream(Compression compressionmethod, string filenameInZip, Stream source, DateTime modTime, string zipfilecomment)
        {
            if (this.fileaccess == FileAccess.Read)
            {
                throw new InvalidOperationException("Writing to zipfile is not alowed");
            }

            long offset;
            if (this.files.Count == 0)
            {
                offset = 0;
            }
            else
            {
                ZipFileEntry last = this.files[this.files.Count - 1];
                offset = last.HeaderOffset + last.HeaderSize;
            }

            // Prepare the fileinfo
            ZipFileEntry zfe = new ZipFileEntry();
            zfe.Method = compressionmethod;
            zfe.EncodeUTF8 = this.EncodeUTF8;
            zfe.FilenameInZip = this.NormalizedFilename(filenameInZip);
            zfe.Comment = (zipfilecomment == null ? string.Empty : zipfilecomment);

            // Even though we write the header now, it will have to be rewritten, since we don't know compressed size or crc.
            zfe.Crc32 = 0;  // to be updated later
            zfe.HeaderOffset = (uint)this.zipfilestream.Position;  // offset within file of the start of this local record
            zfe.ModifyTime = modTime;

            // Write local header
            this.WriteLocalHeader(ref zfe);
            zfe.FileOffset = (uint)this.zipfilestream.Position;

            // Write file to zip (store)
            this.Store(ref zfe, source);
            source.Close();

            this.UpdateCrcAndSizes(ref zfe);
            this.files.Add(zfe);
        }

        /// <summary>
        /// Updates central directory (if pertinent) and close the Zip storage
        /// </summary>
        /// <remarks>This is a required step, unless automatic dispose is used</remarks>
        public void Close()
        {
            if (this.fileaccess != FileAccess.Read)
            {
                uint centralOffset = (uint)this.zipfilestream.Position;
                uint centralSize = 0;
                if (this.centraldirimage != null)
                {
                    this.zipfilestream.Write(this.centraldirimage, 0, this.centraldirimage.Length);
                }

                for (int i = 0; i < this.files.Count; i++)
                {
                    long pos = this.zipfilestream.Position;
                    this.WriteCentralDirRecord(this.files[i]);
                    centralSize += (uint)(this.zipfilestream.Position - pos);
                }

                if (this.centraldirimage != null)
                {
                    this.WriteEndRecord(centralSize + (uint)this.centraldirimage.Length, centralOffset);
                }
                else
                {
                    this.WriteEndRecord(centralSize, centralOffset);
                }
            }

            if (this.zipfilestream != null)
            {
                this.zipfilestream.Flush();
                this.zipfilestream.Dispose();
                this.zipfilestream = null;
            }
        }

        /// <summary>
        /// Read all the file records in the central directory 
        /// </summary>
        /// <returns>List of all entries in directory</returns>
        public List<ZipFileEntry> ReadCentralDir()
        {
            if (this.centraldirimage == null)
            {
                throw new InvalidOperationException("Central directory currently does not exist");
            }

            List<ZipFileEntry> result = new List<ZipFileEntry>();
            for (int pointer = 0; pointer < this.centraldirimage.Length;)
            {
                uint signature = BitConverter.ToUInt32(this.centraldirimage, pointer);
                if (signature != 0x02014b50)
                {
                    break;
                }

                bool encodeUTF8 = (BitConverter.ToUInt16(this.centraldirimage, pointer + 8) & 0x0800) != 0;
                ushort method = BitConverter.ToUInt16(this.centraldirimage, pointer + 10);
                uint modifyTime = BitConverter.ToUInt32(this.centraldirimage, pointer + 12);
                uint crc32 = BitConverter.ToUInt32(this.centraldirimage, pointer + 16);
                uint comprSize = BitConverter.ToUInt32(this.centraldirimage, pointer + 20);
                uint fileSize = BitConverter.ToUInt32(this.centraldirimage, pointer + 24);
                ushort filenameSize = BitConverter.ToUInt16(this.centraldirimage, pointer + 28);
                ushort extraSize = BitConverter.ToUInt16(this.centraldirimage, pointer + 30);
                ushort commentSize = BitConverter.ToUInt16(this.centraldirimage, pointer + 32);
                uint headerOffset = BitConverter.ToUInt32(this.centraldirimage, pointer + 42);
                uint headerSize = (uint)(46 + filenameSize + extraSize + commentSize);

                Encoding encoder = encodeUTF8 ? Encoding.UTF8 : DefaultEncoding;

                ZipFileEntry zfe = new ZipFileEntry();
                zfe.Method = (Compression)method;
                zfe.FilenameInZip = encoder.GetString(this.centraldirimage, pointer + 46, filenameSize);
                zfe.FileOffset = this.GetFileOffset(headerOffset);
                zfe.FileSize = fileSize;
                zfe.CompressedSize = comprSize;
                zfe.HeaderOffset = headerOffset;
                zfe.HeaderSize = headerSize;
                zfe.Crc32 = crc32;
                zfe.ModifyTime = this.DosTimeToDateTime(modifyTime);
                if (commentSize > 0)
                {
                    zfe.Comment = encoder.GetString(this.centraldirimage, pointer + 46 + filenameSize + extraSize, commentSize);
                }

                result.Add(zfe);
                pointer += 46 + filenameSize + extraSize + commentSize;
            }

            return result;
        }

        /// <summary>
        /// Copy the contents of a stored file into a physical file
        /// </summary>
        /// <param name="zfe">Entry information of file to extract</param>
        /// <param name="filename">Name of file to store uncompressed data</param>
        /// <returns>True if success, false if not.</returns>
        /// <remarks>Unique compression methods are Store and Deflate</remarks>
        public bool ExtractFile(ZipFileEntry zfe, string filename)
        {
            // Make sure the parent directory exist
            string path = System.IO.Path.GetDirectoryName(filename);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Check it is directory. If so, do nothing
            if (Directory.Exists(filename))
            {
                return true;
            }

            if (File.Exists(filename))
            {
                return false;
            }

            Stream output = new FileStream(filename, FileMode.Create, FileAccess.Write);
            bool result = this.ExtractFile(zfe, output);
            if (result)
            {
                output.Close();
            }

            File.SetCreationTime(filename, zfe.ModifyTime);
            File.SetLastWriteTime(filename, zfe.ModifyTime);
            return result;
        }

        /// <summary>
        /// Copy the contents of a stored file into an opened stream
        /// </summary>
        /// <param name="zfe">Entry information of file to extract</param>
        /// <param name="stream">Stream to store the uncompressed data</param>
        /// <returns>True if success, false if not.</returns>
        /// <remarks>Unique compression methods are Store and Deflate</remarks>
        public bool ExtractFile(ZipFileEntry zfe, Stream stream)
        {
            if (!stream.CanWrite)
            {
                throw new InvalidOperationException("Stream cannot be written");
            }

            // check signature
            byte[] signature = new byte[4];
            this.zipfilestream.Seek(zfe.HeaderOffset, SeekOrigin.Begin);
            this.zipfilestream.Read(signature, 0, 4);
            if (BitConverter.ToUInt32(signature, 0) != 0x04034b50)
            {
                return false;
            }

            // Select input stream for inflating or just reading
            Stream inStream;
            if (zfe.Method == Compression.Store)
            {
                inStream = this.zipfilestream;
            }
            else if (zfe.Method == Compression.Deflate)
            {
                inStream = new DeflateStream(this.zipfilestream, CompressionMode.Decompress, true);
            }
            else
            {
                return false;
            }

            // Buffered copy
            byte[] buffer = new byte[16384];
            this.zipfilestream.Seek(zfe.FileOffset, SeekOrigin.Begin);
            uint bytesPending = zfe.FileSize;
            while (bytesPending > 0)
            {
                int bytesRead = inStream.Read(buffer, 0, (int)Math.Min(bytesPending, buffer.Length));
                stream.Write(buffer, 0, bytesRead);
                bytesPending -= (uint)bytesRead;
            }

            stream.Flush();
            if (zfe.Method == Compression.Deflate)
            {
                inStream.Dispose();
            }

            return true;
        }

        /// <summary>
        /// Removes one of many files in storage. It creates a new Zip file.
        /// </summary>
        /// <param name="zip">Reference to the current Zip object</param>
        /// <param name="zfes">List of Entries to remove from storage</param>
        /// <returns>True if success, false if not</returns>
        /// <remarks>This method only works for storage of type FileStream</remarks>
        public static bool RemoveEntries(ref ZipStorer zip, List<ZipFileEntry> zfes)
        {
            if (!(zip.zipfilestream is FileStream))
            {
                throw new InvalidOperationException("RemoveEntries is allowed just over streams of type FileStream");
            }

            // Get full list of entries
            List<ZipFileEntry> fullList = zip.ReadCentralDir();
            // In order to delete we need to create a copy of the zip file excluding the selected items
            string tempZipName = Path.GetTempFileName();
            string tempEntryName = Path.GetTempFileName();
            try
            {
                ZipStorer tempZip = ZipStorer.Create(tempZipName, string.Empty);
                foreach (ZipFileEntry zfe in fullList)
                {
                    if (!zfes.Contains(zfe))
                    {
                        if (zip.ExtractFile(zfe, tempEntryName))
                        {
                            tempZip.AddFile(zfe.Method, tempEntryName, zfe.FilenameInZip, zfe.Comment);
                        }
                    }
                }

                zip.Close();
                tempZip.Close();
                File.Delete(zip.filename);
                File.Move(tempZipName, zip.filename);
                zip = ZipStorer.Open(zip.filename, zip.fileaccess);
            }
            catch
            {
                return false;
            }
            finally
            {
                if (File.Exists(tempZipName))
                {
                    File.Delete(tempZipName);
                }

                if (File.Exists(tempEntryName))
                {
                    File.Delete(tempEntryName);
                }
            }

            return true;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Calculate the file offset by reading the corresponding local header
        /// </summary>
        /// <param name="_headerOffset">File pointer offset</param>
        /// <returns></returns>
        private uint GetFileOffset(uint headerOffset)
        {
            byte[] buffer = new byte[2];

            this.zipfilestream.Seek(headerOffset + 26, SeekOrigin.Begin);
            this.zipfilestream.Read(buffer, 0, 2);
            ushort filenameSize = BitConverter.ToUInt16(buffer, 0);
            this.zipfilestream.Read(buffer, 0, 2);
            ushort extraSize = BitConverter.ToUInt16(buffer, 0);

            return (uint)(30 + filenameSize + extraSize + headerOffset);
        }

        /// <summary>
        /// <para>
        /// Local file header:
        /// local file header signature     4 bytes  (0x04034b50)
        /// version needed to extract       2 bytes
        /// general purpose bit flag        2 bytes
        /// compression method              2 bytes
        /// last mod file time              2 bytes
        /// last mod file date              2 bytes
        /// crc-32                          4 bytes
        /// compressed size                 4 bytes
        /// uncompressed size               4 bytes
        /// filename length                 2 bytes
        /// extra field length              2 bytes
        ///
        /// filename (variable size)
        /// extra field (variable size)
        /// </para>
        /// </summary>
        /// <param name="zfe"></param>
        private void WriteLocalHeader(ref ZipFileEntry zfe)
        {
            long pos = this.zipfilestream.Position;
            Encoding encoder = zfe.EncodeUTF8 ? Encoding.UTF8 : DefaultEncoding;
            byte[] encodedFilename = encoder.GetBytes(zfe.FilenameInZip);

            this.zipfilestream.Write(new byte[] { 80, 75, 3, 4, 20, 0 }, 0, 6); // No extra header
            this.zipfilestream.Write(BitConverter.GetBytes((ushort)(zfe.EncodeUTF8 ? 0x0800 : 0)), 0, 2); // filename and comment encoding 
            this.zipfilestream.Write(BitConverter.GetBytes((ushort)zfe.Method), 0, 2);  // zipping method
            this.zipfilestream.Write(BitConverter.GetBytes(this.DateTimeToDosTime(zfe.ModifyTime)), 0, 4); // zipping date and time
            this.zipfilestream.Write(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 0, 12); // unused CRC, un/compressed size, updated later
            this.zipfilestream.Write(BitConverter.GetBytes((ushort)encodedFilename.Length), 0, 2); // filename length
            this.zipfilestream.Write(BitConverter.GetBytes((ushort)0), 0, 2); // extra length

            this.zipfilestream.Write(encodedFilename, 0, encodedFilename.Length);
            zfe.HeaderSize = (uint)(this.zipfilestream.Position - pos);
        }

        /// <summary>
        /// Central directory's File header:
        /// central file header signature   4 bytes  (0x02014b50)
        /// version made by                 2 bytes
        /// version needed to extract       2 bytes
        /// general purpose bit flag        2 bytes
        /// compression method              2 bytes
        /// last mod file time              2 bytes
        /// last mod file date              2 bytes
        /// crc-32                          4 bytes
        /// compressed size                 4 bytes
        /// uncompressed size               4 bytes
        /// filename length                 2 bytes
        /// extra field length              2 bytes
        /// file comment length             2 bytes
        /// disk number start               2 bytes
        /// internal file attributes        2 bytes
        /// external file attributes        4 bytes
        /// relative offset of local header 4 bytes
        ///
        /// filename (variable size)
        /// extra field (variable size)
        /// file comment (variable size)
        /// </summary>
        private void WriteCentralDirRecord(ZipFileEntry zfe)
        {
            Encoding encoder = zfe.EncodeUTF8 ? Encoding.UTF8 : DefaultEncoding;
            byte[] encodedFilename = encoder.GetBytes(zfe.FilenameInZip);
            byte[] encodedComment = encoder.GetBytes(zfe.Comment);

            this.zipfilestream.Write(new byte[] { 80, 75, 1, 2, 23, 0xB, 20, 0 }, 0, 8);
            this.zipfilestream.Write(BitConverter.GetBytes((ushort)(zfe.EncodeUTF8 ? 0x0800 : 0)), 0, 2); // filename and comment encoding 
            this.zipfilestream.Write(BitConverter.GetBytes((ushort)zfe.Method), 0, 2);  // zipping method
            this.zipfilestream.Write(BitConverter.GetBytes(this.DateTimeToDosTime(zfe.ModifyTime)), 0, 4);  // zipping date and time
            this.zipfilestream.Write(BitConverter.GetBytes(zfe.Crc32), 0, 4); // file CRC
            this.zipfilestream.Write(BitConverter.GetBytes(zfe.CompressedSize), 0, 4); // compressed file size
            this.zipfilestream.Write(BitConverter.GetBytes(zfe.FileSize), 0, 4); // uncompressed file size
            this.zipfilestream.Write(BitConverter.GetBytes((ushort)encodedFilename.Length), 0, 2); // Filename in zip
            this.zipfilestream.Write(BitConverter.GetBytes((ushort)0), 0, 2); // extra length
            this.zipfilestream.Write(BitConverter.GetBytes((ushort)encodedComment.Length), 0, 2);

            this.zipfilestream.Write(BitConverter.GetBytes((ushort)0), 0, 2); // disk=0
            this.zipfilestream.Write(BitConverter.GetBytes((ushort)0), 0, 2); // file type: binary
            this.zipfilestream.Write(BitConverter.GetBytes((ushort)0), 0, 2); // Internal file attributes
            this.zipfilestream.Write(BitConverter.GetBytes((ushort)0x8100), 0, 2); // External file attributes (normal/readable)
            this.zipfilestream.Write(BitConverter.GetBytes(zfe.HeaderOffset), 0, 4);  // Offset of header

            this.zipfilestream.Write(encodedFilename, 0, encodedFilename.Length);
            this.zipfilestream.Write(encodedComment, 0, encodedComment.Length);
        }

        /// <summary>
        ///  End of central dir record:
        /// end of central dir signature    4 bytes  (0x06054b50)
        /// number of this disk             2 bytes
        /// number of the disk with the
        /// start of the central directory  2 bytes
        /// total number of entries in
        /// the central dir on this disk    2 bytes
        /// total number of entries in
        /// the central dir                 2 bytes
        /// size of the central directory   4 bytes
        /// offset of start of central
        /// directory with respect to
        /// the starting disk number        4 bytes
        /// zipfile comment length          2 bytes
        /// zipfile comment (variable size)
        /// </summary>
        /// <param name="size"></param>
        /// <param name="offset"></param>
        private void WriteEndRecord(uint size, uint offset)
        {
            Encoding encoder = this.EncodeUTF8 ? Encoding.UTF8 : DefaultEncoding;
            byte[] encodedComment = encoder.GetBytes(this.comment);

            this.zipfilestream.Write(new byte[] { 80, 75, 5, 6, 0, 0, 0, 0 }, 0, 8);
            this.zipfilestream.Write(BitConverter.GetBytes((ushort)this.files.Count + this.existingfiles), 0, 2);
            this.zipfilestream.Write(BitConverter.GetBytes((ushort)this.files.Count + this.existingfiles), 0, 2);
            this.zipfilestream.Write(BitConverter.GetBytes(size), 0, 4);
            this.zipfilestream.Write(BitConverter.GetBytes(offset), 0, 4);
            this.zipfilestream.Write(BitConverter.GetBytes((ushort)encodedComment.Length), 0, 2);
            this.zipfilestream.Write(encodedComment, 0, encodedComment.Length);
        }

        /// <summary>
        /// Copies all source file into storage file
        /// </summary>
        /// <param name="zfe"></param>
        /// <param name="source"></param>
        private void Store(ref ZipFileEntry zfe, Stream source)
        {
            byte[] buffer = new byte[16384];
            int bytesRead;
            uint totalRead = 0;
            Stream outStream;

            long posStart = this.zipfilestream.Position;
            long sourceStart = source.Position;

            if (zfe.Method == Compression.Store)
                outStream = this.zipfilestream;
            else
                outStream = new DeflateStream(this.zipfilestream, CompressionMode.Compress, true);

            zfe.Crc32 = 0 ^ 0xffffffff;
            
            do
            {
                bytesRead = source.Read(buffer, 0, buffer.Length);
                totalRead += (uint)bytesRead;
                if (bytesRead > 0)
                {
                    outStream.Write(buffer, 0, bytesRead);
                    for (uint i = 0; i < bytesRead; i++)
                    {
                        zfe.Crc32 = ZipStorer.crctable[(zfe.Crc32 ^ buffer[i]) & 0xFF] ^ (zfe.Crc32 >> 8);
                    }
                }
            } while (bytesRead == buffer.Length);
            outStream.Flush();

            if (zfe.Method == Compression.Deflate)
                outStream.Dispose();

            zfe.Crc32 ^= 0xffffffff;
            zfe.FileSize = totalRead;
            zfe.CompressedSize = (uint)(this.zipfilestream.Position - posStart);

            // Verify for real compression
            if (zfe.Method == Compression.Deflate && !this.ForceDeflating && source.CanSeek && zfe.CompressedSize > zfe.FileSize)
            {
                // Start operation again with Store algorithm
                zfe.Method = Compression.Store;
                this.zipfilestream.Position = posStart;
                this.zipfilestream.SetLength(posStart);
                source.Position = sourceStart;
                this.Store(ref zfe, source);
            }
        }

        /// <summary>
        /// <para>
        /// DOS Date and time:
        /// MS-DOS date. The date is a packed value with the following format. Bits Description 
        ///     0-4 Day of the month (1–31) 
        ///     5-8 Month (1 = January, 2 = February, and so on) 
        ///     9-15 Year offset from 1980 (add 1980 to get actual year) 
        /// MS-DOS time. The time is a packed value with the following format. Bits Description 
        ///     0-4 Second divided by 2 
        ///     5-10 Minute (0–59) 
        ///     11-15 Hour (0–23 on a 24-hour clock) 
        /// </para>
        /// </summary>
        /// <param name="dtdostime"></param>
        /// <returns></returns>
        private uint DateTimeToDosTime(DateTime dtdostime)
        {
            return (uint)(
                (dtdostime.Second / 2) | (dtdostime.Minute << 5) | (dtdostime.Hour << 11) | 
                (dtdostime.Day << 16) | (dtdostime.Month << 21) | ((dtdostime.Year - 1980) << 25));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtdostime"></param>
        /// <returns></returns>
        private DateTime DosTimeToDateTime(uint dtdostime)
        {
            return new DateTime(
                (int)(dtdostime >> 25) + 1980,
                (int)(dtdostime >> 21) & 15,
                (int)(dtdostime >> 16) & 31,
                (int)(dtdostime >> 11) & 31,
                (int)(dtdostime >> 5) & 63,
                (int)(dtdostime & 31) * 2);
        }

        /// <summary>
        /// <para>
        ///  CRC32 algorithm
        /// The 'magic number' for the CRC is 0xdebb20e3.  
        /// The proper CRC pre and post conditioning
        /// is used, meaning that the CRC register is
        /// pre-conditioned with all ones (a starting value
        /// of 0xffffffff) and the value is post-conditioned by
        /// taking the one's complement of the CRC residual.
        /// If bit 3 of the general purpose flag is set, this
        /// field is set to zero in the local header and the correct
        /// value is put in the data descriptor and in the central
        /// directory.</para>
        /// </summary>
        /// <param name="zfe"></param>
        private void UpdateCrcAndSizes(ref ZipFileEntry zfe)
        {
            long lastPos = this.zipfilestream.Position;  // remember position

            this.zipfilestream.Position = zfe.HeaderOffset + 8;
            this.zipfilestream.Write(BitConverter.GetBytes((ushort)zfe.Method), 0, 2);  // zipping method

            this.zipfilestream.Position = zfe.HeaderOffset + 14;
            this.zipfilestream.Write(BitConverter.GetBytes(zfe.Crc32), 0, 4);  // Update CRC
            this.zipfilestream.Write(BitConverter.GetBytes(zfe.CompressedSize), 0, 4);  // Compressed size
            this.zipfilestream.Write(BitConverter.GetBytes(zfe.FileSize), 0, 4);  // Uncompressed size

            this.zipfilestream.Position = lastPos;  // restore position
        }

        /// <summary>
        /// Replaces backslashes with slashes to store in zip header.
        /// </summary>
        /// <param name="zipfilename"></param>
        /// <returns></returns>
        private string NormalizedFilename(string zipfilename)
        {
            string filename = zipfilename.Replace('\\', '/');

            int pos = filename.IndexOf(':');
            if (pos >= 0)
                filename = filename.Remove(0, pos + 1);
            return filename.Trim('/');
        }

        /// <summary>
        ///  Reads the end-of-central-directory record.
        /// </summary>
        /// <returns></returns>
        private bool ReadFileInfo()
        {
            if (this.zipfilestream.Length < 22)
                return false;

            try
            {
                this.zipfilestream.Seek(-17, SeekOrigin.End);
                BinaryReader br = new BinaryReader(this.zipfilestream);
                do
                {
                    this.zipfilestream.Seek(-5, SeekOrigin.Current);
                    uint sig = br.ReadUInt32();
                    if (sig == 0x06054b50)
                    {
                        this.zipfilestream.Seek(6, SeekOrigin.Current);

                        ushort entries = br.ReadUInt16();
                        int centralSize = br.ReadInt32();
                        uint centralDirOffset = br.ReadUInt32();
                        ushort commentSize = br.ReadUInt16();

                        // check if comment field is the very last data in file
                        if (this.zipfilestream.Position + commentSize != this.zipfilestream.Length)
                            return false;

                        // Copy entire central directory to a memory buffer
                        this.existingfiles = entries;
                        this.centraldirimage = new byte[centralSize];
                        this.zipfilestream.Seek(centralDirOffset, SeekOrigin.Begin);
                        this.zipfilestream.Read(this.centraldirimage, 0, centralSize);

                        // Leave the pointer at the begining of central dir, to append new files
                        this.zipfilestream.Seek(centralDirOffset, SeekOrigin.Begin);
                        return true;
                    }
                } while (this.zipfilestream.Position > 0);
            }
            catch 
            {
                // todo specificate
            }

            return false;
        }
        #endregion

        #region IDisposable Members
        /// <summary>
        /// Closes the Zip file stream
        /// </summary>
        public void Dispose()
        {
            this.Close();
        }
        #endregion
    }
}
