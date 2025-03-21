using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class EncryptionSystem : MonoBehaviour
{
    // Fix later
    public static byte[] EncryptStringToBytes_Aes(string data)
    {
        var hash = ComputeHash(data);
        var dataWithHash = hash + "|" + data;

        using var aesAlg = Aes.Create();
        aesAlg.GenerateKey();
        aesAlg.GenerateIV();

        var key = Convert.ToBase64String(aesAlg.Key);
        var iv = Convert.ToBase64String(aesAlg.IV);

        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using (var msEncrypt = new MemoryStream())
        {
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using (var swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(dataWithHash);
                }

                return msEncrypt.ToArray();
            }
        }
    }

    private static string ComputeHash(string data)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));

            // Convert hash to string representation
            var builder = new StringBuilder();
            for (var i = 0; i < hashBytes.Length; i++) builder.Append(hashBytes[i].ToString("x2"));

            return builder.ToString();
        }
    }
}