using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BiaBraga.Repository.Classes
{
    public static class Encript
    {
        public static string _key = string.Empty;
        private static readonly SymmetricAlgorithm _algorithm = new RC2CryptoServiceProvider {
            Mode = CipherMode.CBC,
            IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9 }
    };

        private static byte[] GetKey()
        {
            string salt = string.Empty;
            if (_algorithm.LegalKeySizes.Length > 0)
            {
                int keySize = _key.Length * 8;
                int minSize = _algorithm.LegalKeySizes[0].MinSize;
                int maxSize = _algorithm.LegalKeySizes[0].MaxSize;
                int skipSize = _algorithm.LegalKeySizes[0].SkipSize;
                if (keySize > maxSize)
                {
                    _key = _key.Substring(0, maxSize / 8);
                }
                else if (keySize < maxSize)
                {
                    int validSize = (keySize <= minSize) ? minSize : (keySize - keySize % skipSize) + skipSize;
                    if (keySize < validSize)
                    {
                        // _key = _key.PadRight(validSize / 8, "*");
                    }
                }
            }
            PasswordDeriveBytes key = new PasswordDeriveBytes(_key, ASCIIEncoding.ASCII.GetBytes(salt));
            return key.GetBytes(_key.Length);
        }

        public static string Encrypt(string text)
        {
            byte[] plainByte = Encoding.UTF8.GetBytes(text);
            byte[] keyByte = GetKey();
            _algorithm.Key = keyByte;
            ICryptoTransform cryptoTransform = _algorithm.CreateEncryptor();
            MemoryStream _memoryStream = new MemoryStream();
            CryptoStream _cryptoStream = new CryptoStream(_memoryStream, cryptoTransform, CryptoStreamMode.Write);
            _cryptoStream.Write(plainByte, 0, plainByte.Length);
            _cryptoStream.FlushFinalBlock();

            byte[] cryptoByte = _memoryStream.ToArray();
            return Convert.ToBase64String(cryptoByte, 0, cryptoByte.GetLength(0));
        }

        public static string Decrypt(string textCript)
        {
            byte[] cryptoByte = Convert.FromBase64String(textCript);
            byte[] keyByte = GetKey();

            _algorithm.Key = keyByte;
            ICryptoTransform cryptoTransform = _algorithm.CreateDecryptor();
            try
            {
                MemoryStream _memoryStream = new MemoryStream(cryptoByte, 0, cryptoByte.Length);
                CryptoStream _cryptoStream = new CryptoStream(_memoryStream, cryptoTransform, CryptoStreamMode.Read);
                StreamReader _streamReader = new StreamReader(_cryptoStream);
                return _streamReader.ReadToEnd();
            }
            catch
            {
                return null;
            }
        }

        public static string HashValue(string value)
        {
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] hashBytes;
            using (HashAlgorithm hash = SHA1.Create())
                hashBytes = hash.ComputeHash(encoding.GetBytes(value));

            StringBuilder hashValue = new StringBuilder(hashBytes.Length * 2);
            foreach (byte b in hashBytes)
            {
                hashValue.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", b);
            }

            return hashValue.ToString();
        }


    }
}
