using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Web;

namespace Music_Store.Security
{
    public class Fortify
    {
        private readonly string Key = "12345678ABCDEFGH";
        private readonly string IV = "12345678";
        private RijndaelManaged rijndaelManaged;
        private readonly Func<string, byte[]> GetBytes = text 
            => Encoding.Unicode.GetBytes(text);
        
        /// <summary>
        /// Encrypt password
        /// </summary>
        /// <param name="text"> Orign password </param>
        /// <returns> Encrypt password </returns>
        public string Encryt(string text)
        {
            using(rijndaelManaged = new RijndaelManaged())
            {
                rijndaelManaged.Key = GetBytes(Key);
                rijndaelManaged.IV = GetBytes(IV);

                byte[] encryted = EncryptTextToByte(text, rijndaelManaged.Key, rijndaelManaged.IV);

                return Convert.ToBase64String(encryted, 0, encryted.Length);
            }
        }

        /// <summary>
        /// Encrypt text to byte array
        /// </summary>
        /// <param name="text"> plaintext </param>
        /// <param name="key">  </param>
        /// <param name="iv">  </param>
        /// <returns> byte array </returns>
        private byte[] EncryptTextToByte(string text, byte[] key, byte[] iv)
        {
            byte[] encrypted;

            using(rijndaelManaged = new RijndaelManaged())
            {
                rijndaelManaged.Key = key;
                rijndaelManaged.IV = iv;
                rijndaelManaged.Mode = CipherMode.CFB;

                ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor(rijndaelManaged.Key, rijndaelManaged.IV);

                using(MemoryStream ms = new MemoryStream())
                    using(CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using(StreamWriter sw = new StreamWriter(cs))
                            sw.Write(text);
                        encrypted = ms.ToArray();
                    }
            }

            return encrypted;
        }

        /// <summary>
        /// Decrypt password
        /// </summary>
        /// <param name="encrypted"> Encrypted password </param>
        /// <returns> plain text </returns>
        public string Decrypt(string encrypted)
        {
            byte[] key = GetBytes(Key);
            byte[] iv = GetBytes(IV);
            byte[] encryptedText = Convert.FromBase64String(encrypted);

            using(rijndaelManaged = new RijndaelManaged())
                return DecryptTextFromBytes(encryptedText, key, iv);
        }

        /// <summary>
        /// Decrypt text from byte array 
        /// </summary>
        /// <param name="cipherText"> Encrypt text </param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns> Decrypted text </returns>
        private string DecryptTextFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            string plainText = string.Empty;

            using(rijndaelManaged = new RijndaelManaged())
            {
                rijndaelManaged.Key = key;
                rijndaelManaged.IV = iv;
                rijndaelManaged.Mode = CipherMode.CFB;

                ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor(
                    rijndaelManaged.Key, rijndaelManaged.IV);

                using(MemoryStream ms = new MemoryStream(cipherText))
                {
                    using(CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using(StreamReader sr = new StreamReader(cs))
                        {
                            plainText = sr.ReadToEnd();
                        }
                    }
                }
            }

            return plainText;
        }
    }
}