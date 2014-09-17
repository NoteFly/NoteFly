//-----------------------------------------------------------------------
// <copyright file="RSAVerify.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2012-2013  Tom
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------
namespace NoteFly
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// This class can be used to verify a file or string with the NoteFly public key to check 
    /// if the file or data is really coming from the developer who owns the NoteFly private key.
    /// <remarks>
    /// No RSA encryption is being used, only RSA signature checking is used.
    /// </remarks>
    /// </summary>
    public class RSAVerify
    {
        /// <summary>
        /// The RSA NoteFly keysize used.
        /// </summary>
        private const int RSAKEYSIZE = 3072;

        /// <summary>
        /// The RSA NoteFly public key.
        /// </summary>
        private const string XMLRSAPUBLICKEY = "<RSAKeyValue><Modulus>zxrgHdkENFOlcTI0AxDMhLgNzduTikBd1EX9gwWz+vTxgfR3RM2P3M8ImNL6QYk0Sch78wv3zac3pjWINoqpazFBwb1A0jawJUxgftfbEmfDvBuK58f+FOeE4KxYE9za+jZyxCn6bbj/M7cK2wgo8Mhmq/WP9aFUf8dcVcQH+3cqNt56zLSUXHcIdexZwVRv9SbzlY6MtlmRuzKO++O3ersXWuJPf8DJu98bAP2W0B3puPhNtXb6SnBF/FO9BZUDcCYNrJ0IuwyMA1nBm8aFPfok12ohzSAP1r5Hs0yVtmOXucWBdv7lim9jhL1aqXsh0U2aT0zxBaLWZsU7WwX8Iikb4ZEXkTsmBRHhPGfQ81P8zZAlgmmmCC+jLRK93cPZYcH9t6UFcdGgaDAurck9bmB+Mb6bahkv5eiumRbDXixLN3jVIDtOjI2Bg4KvdgYKJskuXpICadF/rC/ZNq7ZtONJ7wrVUu+Q/dRONE3okoCeZjK7JUdlVdWGVGG7fgZj</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        /// <summary>
        /// RSACryptoServiceProvider
        /// </summary>
        private RSACryptoServiceProvider rsaCryptoServiceProvider = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="RSAVerify" /> class.
        /// </summary>
        public RSAVerify()
        {
            this.rsaCryptoServiceProvider = new RSACryptoServiceProvider(RSAKEYSIZE);
        }

        /// <summary>
        /// Check a signature for a file and display a error if signature is invalid.
        /// </summary>
        /// <param name="filepath">The full file path to the file to check the signature from.</param>
        /// <returns>True if RSA signature of hash of file is valid for a file.</returns>
        public bool CheckFileSignatureAndDisplayErrors(string filepath, string signature)
        {
            if (this.CheckSignatureFilehash(filepath, signature))
            {
                Log.Write(LogType.info, "Valid RSA file hash signature of file " + filepath);
                return true;
            }
            else
            {
                Log.Write(LogType.error, "Invalid RSA file hash signature of file " + filepath);
                System.Windows.Forms.MessageBox.Show("Signature of file is not valid.", "security issue", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Stop);
                return false;
            }
        }

        /// <summary>
        /// Check a signature for a hash of a file.
        /// </summary>
        /// <param name="filepath">The full file path to the file to check the signature from the hash of the file.</param>
        /// <returns>True if the hash of the file is valid by the signature</returns>
        public bool CheckSignatureFilehash(string filepath, string signature)
        {
            string sha256filehash = this.Sha256file(filepath);
            return this.IsValidSignature(sha256filehash, SHA256.Create(), signature);
        }

        /// <summary>
        /// Check data and with the signature.
        /// </summary>
        /// <param name="data">The data to check the signature from.</param>
        /// <param name="hashinsignatureused">The signature of the hash.</param>
        /// <returns>True if the signature is valid for the data.</returns>
        private bool IsValidSignature(string data, object hashinsignatureused, string signature)
        {
            this.rsaCryptoServiceProvider.FromXmlString(XMLRSAPUBLICKEY);
            byte[] databytes = Encoding.UTF32.GetBytes(data);
            byte[] signaturebytes = Convert.FromBase64String(signature);
            return this.rsaCryptoServiceProvider.VerifyData(databytes, hashinsignatureused, signaturebytes);
        }

        /// <summary>
        /// Generate a SHA 256 hash for a file.
        /// </summary>
        /// <param name="file">The full file path to create the hash from.</param>
        /// <returns>The SHA256 hash as string</returns>
        private string Sha256file(string file)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                using (SHA256Managed sha256 = new SHA256Managed())
                {
                    byte[] hash = sha256.ComputeHash(fs);
                    StringBuilder formatted = new StringBuilder(hash.Length);
                    foreach (byte b in hash)
                    {
                        formatted.AppendFormat("{0:X2}", b);
                    }

                    return formatted.ToString();
                }
            }
        }
    }
}