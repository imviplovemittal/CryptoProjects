using System;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;

namespace P3
{
    class Program
    {

        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
                {
                    if (plainText == null || plainText.Length <= 0)
                        throw new ArgumentNullException("plainText");
                    if (Key == null || Key.Length <= 0)
                        throw new ArgumentNullException("Key");
                    if (IV == null || IV.Length <= 0)
                        throw new ArgumentNullException("IV");
                    byte[] encrypted;

                    // Create an Aes object
                    // with the specified key and IV.
                    using (Aes aesAlg = Aes.Create())
                    {
                        aesAlg.Key = Key;
                        aesAlg.IV = IV;

                        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                        using (MemoryStream msEncrypt = new MemoryStream())
                        {
                            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                            {
                                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                                {
                                    swEncrypt.Write(plainText);
                                }
                                encrypted = msEncrypt.ToArray();
                            }
                        }
                    }

                    return encrypted;
                }

        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
                {
                    if (cipherText == null || cipherText.Length <= 0)
                        throw new ArgumentNullException("cipherText");
                    if (Key == null || Key.Length <= 0)
                        throw new ArgumentNullException("Key");
                    if (IV == null || IV.Length <= 0)
                        throw new ArgumentNullException("IV");

                    string plaintext = null;

                    using (Aes aesAlg = Aes.Create())
                    {
                        aesAlg.Key = Key;
                        aesAlg.IV = IV;

                        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                        using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                        {
                            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                            {
                                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                                {
                                    plaintext = srDecrypt.ReadToEnd();
                                }
                            }
                        }
                    }

                    return plaintext;
                }

        static void Main(string[] args)
        {

            string[] ivString = args[0].Split(' ');

            byte[] ivBytes = new byte[16];

            for (int i = 0; i < ivBytes.Length; i++)
            {
               ivBytes[i] = Convert.ToByte(ivString[i], 16);
            }

            int N_e = Int32.Parse(args[3]);

            BigInteger N = BigInteger.Pow(BigInteger.Parse("2"), N_e) - BigInteger.Parse(args[4]);

            BigInteger gymodN = BigInteger.Parse(args[6]);

            BigInteger key = BigInteger.ModPow(gymodN, BigInteger.Parse(args[5]), N);

            byte[] keyBytes = key.ToByteArray();

            byte[] encrypted = EncryptStringToBytes_Aes(args[8], keyBytes, ivBytes);

            string outputRawEncrypted = BitConverter.ToString(encrypted).Replace("-", " ");

            string[] encryptedRawString = args[7].Split(' ');

            byte[] encryptedString = new byte[encryptedRawString.Length];

            for (int i = 0; i < encryptedRawString.Length; i++)
            {
               encryptedString[i] = Convert.ToByte(encryptedRawString[i], 16);
            }

            string outputDecryptedString = DecryptStringFromBytes_Aes(encryptedString, keyBytes, ivBytes);

            Console.WriteLine(outputDecryptedString + "," + outputRawEncrypted);

        }
    }
}
