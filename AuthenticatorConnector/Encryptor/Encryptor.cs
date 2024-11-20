using Firebase.Auth.Requests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Documents;
using System.Windows.Input;

namespace AuthenticatorConnector.Encryptor
{
    public class Encryptor
    {

        public static double GetKeyLength(string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            return keyBytes.Length;
        }

        public static bool KeyIsValid(string key) 
        {
            var keyLength = GetKeyLength(key);
            if (keyLength != 16 && keyLength != 24 && keyLength != 32)
                return false;
            return true;
        }

        public static string EncryptString(string plainText, string key)
        {
            if(!KeyIsValid(key)) throw new Exception("Invalid Key size - must be 16, 24, or 32 bytes for AES.");

            using (Aes aes = Aes.Create())
            {

                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.GenerateIV();
                aes.Mode = CipherMode.CBC;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream())
                {
                    ms.Write(aes.IV, 0, aes.IV.Length);
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string DecryptString(string cipherText, string key)
        {
            if (!KeyIsValid(key)) throw new Exception("Invalid Key size - must be 16, 24, or 32 bytes for AES.");

            byte[] fullCipher = Convert.FromBase64String(cipherText);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                byte[] iv = new byte[aes.BlockSize / 8];
                Array.Copy(fullCipher, 0, iv, 0, iv.Length);
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream(fullCipher, iv.Length, fullCipher.Length - iv.Length))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
