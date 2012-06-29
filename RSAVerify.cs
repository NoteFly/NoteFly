//-----------------------------------------------------------------------
// <copyright file="RSAVerify.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2012  Tom
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
// </copyright>
//-----------------------------------------------------------------------
namespace NoteFly
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Security.Cryptography;
    using System.IO;

    /// <summary>
    /// This class can be used to verify a file or string with the NoteFly public key to check 
    /// if the file or data is really coming from the developer who owns the NoteFly private key.
    /// No encryption, only signature checking is used.
    /// <remarks>
    /// Patent on RSA is waived after 6, 2000.
    /// source: http://www.rsa.com/rsalabs/node.asp?id=2322
    /// </remarks>
    /// </summary>
    public class RSAVerify
    {
        /// <summary>
        /// The RSA NoteFly keysize used.
        /// </summary>
        private const int rsakeysize = 2048;

        /// <summary>
        /// The RSA NoteFly public key.
        /// </summary>
        private const string xmlrsapublickey = "<RSAKeyValue><Modulus>7vgIwkge+r8xMeUN+eSi9SANa6rlzmCH6Z/hjEkb5DKqu/qluPL7f4YVCVIjdz6jf8YEVMdGF2IQGxasDPDeJYdMQ2QSzj3Yjd34VojR4QH5pxUIA8N7eIGnvpOdqrkEUG1l8+3IPjPIRAA9yxthhXSGZ11VW3yrJ9NEJ0iSuRDjUKgbOOrOF2RP9nxmlQK+yRccj+3Ei6MjDYwoEchOn2hhwy4Nr1i17CRPf7247/1HVSRV0a7/s0TNT6a5jj5AU0udFTT9ijmDmipZvJKC+pzl3MPnO8UAdNJiHpurAtni/YuyJ60EGKiUBNtrEI46BN/488PVsD9fqa2rf/oYuQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        /// <summary>
        /// signature
        /// </summary>
        private string signature = string.Empty;

        /// <summary>
        /// Create a new instance of RSAVerify class.
        /// </summary>
        /// <param name="signature"></param>
        public RSAVerify(string signature)
        {
            this.signature = signature;
            // /*
            if (String.IsNullOrEmpty(signature))
            {
                System.Windows.Forms.MessageBox.Show("No signature used, cannot check intergity of download with RSA.", "no signature", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
            // */
        }

        /// <summary>
        /// Check if their is a signature
        /// </summary>
        public bool IsSignatureSet
        {
            get
            {
                return !String.IsNullOrEmpty(signature);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        public bool CheckFileSignatureAndDisplayErrors(string filepath)
        {
            if (this.CheckSignatureFilehash(filepath))
            {
                Log.Write(LogType.info, "Valid RSA file hash signature of file " + filepath);
                return true;
            }
            else
            {
                Log.Write(LogType.error, "Invalid RSA file hash signature of file " + filepath);
                System.Windows.Forms.MessageBox.Show("Signature of file is not valid.", "security issue", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        public bool CheckSignatureFilehash(string filepath)
        {
            string sha256filehash = this.Sha256file(filepath);
            return this.IsValidSignature(sha256filehash, SHA256.Create());
        }


        /// <summary>
        /// Check data and with the signature.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        private bool IsValidSignature(string data, object hashinsignatureused)
        {
            RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider(rsakeysize);
            rsaCryptoServiceProvider.FromXmlString(xmlrsapublickey);
            byte[] databytes = Encoding.UTF32.GetBytes(data);
            byte[] signaturebytes = Convert.FromBase64String(signature);
            return rsaCryptoServiceProvider.VerifyData(databytes, hashinsignatureused, signaturebytes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
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