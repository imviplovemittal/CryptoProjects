using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace P2
{
    public static class Global
        {
            public static bool stopFinal;
        }
    class Program
    {
        // This function will help us get the input from the command line
        public static string getInputFromCommandLine(string[] args)
        {
            string input = "";
            if (args.Length == 1)
            {
                input = args[0];
            }
            else
            {
                Console.WriteLine("Not enough or too many inputs provided after 'dotnet run' ");
            }
            return input;
        }

        public static string CreateMD5(string input, string salt)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                byte[] concatArr = new byte[inputBytes.Length + 1];

                for (int i = 0; i < inputBytes.Length; i++) {
                    concatArr[i] = inputBytes[i];
                }

                concatArr[inputBytes.Length] = Convert.ToByte(salt, 16);

                byte[] hashBytes = md5.ComputeHash(concatArr);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 5; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        static void getAllKLengthRec(string set,
                                       String prefix,
                                       int n, int k, IDictionary<string, string> hashesWithPasswords,
                                       string salt)
        {

            if (k == 0)
            {
                string hash = CreateMD5(prefix, salt);
                if (hashesWithPasswords.ContainsKey(hash)) {
                    Global.stopFinal = true;
                    Console.WriteLine(prefix + "," + hashesWithPasswords[hash]);
                } else {
                    hashesWithPasswords[hash] = prefix;
                }
                return;
            }

            for (int i = 0; i < n; ++i)
            {
                if (Global.stopFinal) {
                    break;
                }
                String newPrefix = prefix + set[i];

                getAllKLengthRec(set, newPrefix,
                                        n, k - 1, hashesWithPasswords, salt);
            }
        }

        static void Main(string[] args)
        {
            Global.stopFinal = false;

            string salt = getInputFromCommandLine(args);

            string alphanumeric_characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            IDictionary<string, string> hashesWithPasswords = new Dictionary<string, string>();

            hashesWithPasswords.ContainsKey("hash");

            List<string> allStrings = new List<string>();

            getAllKLengthRec(alphanumeric_characters, "", 36, 10, hashesWithPasswords, salt);
        }

    }
}
