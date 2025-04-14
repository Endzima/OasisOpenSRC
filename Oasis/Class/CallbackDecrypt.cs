using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class CallbackDecryption
{
    private static readonly byte[] keyBytes = SHA256.HashData(Encoding.UTF8.GetBytes(
        "3533c9c5ebe7ee18cfe32855c88651341cfc82389fef27e212b5a8349d0855c3"));

    public static string DecryptData(byte[] encryptedData)
    {
        try
        {
            byte[] ivBytes = keyBytes[..16]; // First 16 bytes for IV

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes; // Use 32-byte key
                aes.IV = ivBytes;   // Use 16-byte IV
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                using (MemoryStream ms = new MemoryStream(encryptedData))
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (StreamReader reader = new StreamReader(cs))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Decryption failed: " + ex.Message);
        }
    }
}