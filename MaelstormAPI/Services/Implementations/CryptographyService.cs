using System;
using System.IO;
using MaelstormApi.Services.Abstractions;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace MaelstormAPI.Services.Implementations
{
    public class CryptographyService:ICryptographyService
    {
       public byte[] AesEncryptBytes(byte[] bytes, byte[] key, byte[] iv, int keyBitSize = 128)
        {            
            byte[] result;
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = keyBitSize;
                aes.BlockSize = 128;
                aes.Key = key;

                using (var encryptor = aes.CreateEncryptor(aes.Key, iv))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(bytes, 0, bytes.Length);
                            cryptoStream.FlushFinalBlock();
                            result = memoryStream.ToArray();
                        }
                    }
                }
            }
            return result;
        }

       public byte[] AesDecryptBytes(byte[] bytes, byte[] key, byte[] iv, int keySize = 128)
       {
           byte[] result;
           using (Aes aes = Aes.Create())
           {
               aes.KeySize = keySize;
               aes.BlockSize = 128;
               aes.Key = key;

               using (var decryptor = aes.CreateDecryptor(aes.Key, iv))
               {
                   using (var memoryStream = new MemoryStream())
                   {
                       using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
                       {
                           cryptoStream.Write(bytes, 0, bytes.Length);
                           cryptoStream.FlushFinalBlock();
                           result = memoryStream.ToArray();
                       }
                   }
               }
           }
           return result;
       } 

        public byte[] GetRandomBytes(int size = 32)
        {
            var salt = new byte[size];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }
            return salt;
        }

        public string GetRandomBase64String(int byteArraySize = 32)
        {           
            return Convert.ToBase64String(GetRandomBytes(byteArraySize));
        }

        public byte[] Pbkdf2(string password, byte[] salt, int numBytes = 32)
        {
            return KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: numBytes
                );
        }

        public byte[] GenerateIV()
        {
            using var aes = Aes.Create();
            aes.GenerateIV();
            return aes.IV;
        }
    }
}