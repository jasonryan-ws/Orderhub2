using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Utilities.Cryptography
{
    public static class AES
    {
        public static string Encrypt(string plainText, string publicKey, string privateKey)
        {
            try
            {
                var encrypted = string.Empty;

                byte[] privateKeyByte = { };
                privateKeyByte = Encoding.UTF8.GetBytes(privateKey);
                byte[] publicKeyByte = { };
                publicKeyByte = Encoding.UTF8.GetBytes(publicKey);
                MemoryStream ms = null;
                CryptoStream cs = null;
                var inputByteArray = Encoding.UTF8.GetBytes(plainText);
                using (var des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateEncryptor(publicKeyByte, privateKeyByte), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    encrypted = Convert.ToBase64String(ms.ToArray());
                }
                return encrypted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public static string Decrypt(string encryptedText, string publicKey, string privateKey)
        {
            try
            {
                var decrypted = string.Empty;
                byte[] privateKeyByte = { };
                privateKeyByte = Encoding.UTF8.GetBytes(privateKey);
                byte[] publicKeybyte = { };
                publicKeybyte = Encoding.UTF8.GetBytes(publicKey);
                MemoryStream ms = null;
                CryptoStream cs = null;
                var inputByteArray = new byte[encryptedText.Replace(" ", "+").Length];
                inputByteArray = Convert.FromBase64String(encryptedText.Replace(" ", "+"));
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    ms = new MemoryStream();
                    cs = new CryptoStream(ms, des.CreateDecryptor(publicKeybyte, privateKeyByte), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    Encoding encoding = Encoding.UTF8;
                    decrypted = encoding.GetString(ms.ToArray());
                }
                return decrypted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

    }
}