using System;
using System.IO;
using System.Security.Cryptography;

namespace P1_2
{
    class Program
    {
        public static Tuple<string,string> getInputFromCommandLine(string[] args)
        {
            string input_1="", input_2="";
            if(args.Length == 2)
            {
                input_1 = args[0];
                input_2 = args[1];
            }else
            {
                Console.WriteLine("Either not enough inputs or too many inputs");
            }
            return Tuple.Create(input_1, input_2);
        }

        // TODO: put your solution code in the solve function and have it return the seed. In the example, the seed returned was 26564295
        private static double Solve(string plaintext, string ciphertext)
        {
            DateTime dt = new DateTime(2020, 7, 3, 11, 0, 0);
            TimeSpan ts = dt.Subtract(new DateTime(1970, 1, 1));
            int start = (int)ts.TotalMinutes;

            DateTime dt2 = new DateTime(2020, 7, 4, 11, 0, 0);
            TimeSpan ts2 = dt2.Subtract(new DateTime(1970, 1, 1));
            int end = (int)ts2.TotalMinutes;

            for (int i = start; i <= end; i++) {
                Random rng = new Random(i);
                byte[] key = BitConverter.GetBytes(rng.NextDouble());
                string encrypted = Encrypt(key, plaintext);
                if(String.Equals(encrypted, ciphertext)) {
                    return i;
                }

            }

            return -1;
        }

        private static string Encrypt(byte[] key, string secretString)
        {
            DESCryptoServiceProvider csp = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, csp.CreateEncryptor(key, key), CryptoStreamMode.Write);
            StreamWriter sw = new StreamWriter(cs);
            sw.Write(secretString);
            sw.Flush();
            cs.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        }

        static void Main(string[] args)
        {

            Tuple<string, string> commandlineInputs = getInputFromCommandLine(args);
            string plaintext = commandlineInputs.Item1;
            string ciphertext = commandlineInputs.Item2;

            var solution = Solve(plaintext, ciphertext);

            Console.WriteLine(solution);
        }
    }
}
