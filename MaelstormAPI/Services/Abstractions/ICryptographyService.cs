namespace MaelstormApi.Services.Abstractions
{
    public interface ICryptographyService
    {
        byte[] AesEncryptBytes(byte[] bytes, byte[] key, byte[] iv, int keyBitSize = 128);
        byte[] AesDecryptBytes(byte[] bytes, byte[] key, byte[] iv, int keyBitSize = 128);
        byte[] GetRandomBytes(int size = 32);
        string GetRandomBase64String(int byteArraySize = 32);
        byte[] Pbkdf2(string password, byte[] salt, int numBytes = 32);
        byte[] GenerateIV();
    }
}