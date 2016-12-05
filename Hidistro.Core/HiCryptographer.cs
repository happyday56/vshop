namespace Hidistro.Core
{
    using System;
    using System.Configuration;
    using System.Security.Cryptography;
    using System.Text;

    public sealed class HiCryptographer
    {
        private static byte[] CreateHash(byte[] plaintext)
        {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            return provider.ComputeHash(plaintext);
        }

        public static string CreateHash(string plaintext)
        {
            byte[] buffer = CreateHash(Encoding.ASCII.GetBytes(plaintext));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString("x2"));
            }
            return builder.ToString();
        }

        public static string Decrypt(string text)
        {
            using (RijndaelManaged managed = new RijndaelManaged())
            {
                managed.Key = Convert.FromBase64String(ConfigurationManager.AppSettings["Key"]);
                managed.IV = Convert.FromBase64String(ConfigurationManager.AppSettings["IV"]);
                ICryptoTransform transform = managed.CreateDecryptor();
                byte[] inputBuffer = Convert.FromBase64String(text);
                byte[] bytes = transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
                transform.Dispose();
                return Encoding.UTF8.GetString(bytes);
            }
        }

        public static string Encrypt(string text)
        {
            using (RijndaelManaged managed = new RijndaelManaged())
            {
                managed.Key = Convert.FromBase64String(ConfigurationManager.AppSettings["Key"]);
                managed.IV = Convert.FromBase64String(ConfigurationManager.AppSettings["IV"]);
                ICryptoTransform transform = managed.CreateEncryptor();
                byte[] bytes = Encoding.UTF8.GetBytes(text);
                byte[] inArray = transform.TransformFinalBlock(bytes, 0, bytes.Length);
                transform.Dispose();
                return Convert.ToBase64String(inArray);
            }
        }

        public static string Md5Encrypt(string sourceData)
        {
            string str3;
            Encoding encoding = new UTF8Encoding();
            byte[] bytes = encoding.GetBytes("12345678");
            byte[] rgbIV = new byte[] { 1, 2, 3, 4, 5, 6, 8, 7 };
            string s = sourceData;
            try
            {
                ICryptoTransform transform = new DESCryptoServiceProvider().CreateEncryptor(bytes, rgbIV);
                byte[] inputBuffer = encoding.GetBytes(s);
                str3 = Convert.ToBase64String(transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
            }
            catch
            {
                throw;
            }
            return str3;
        }
    }
}

