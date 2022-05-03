using System;
using System.IO;
using System.Collections;

namespace P1_1
{
    class Program
    {
        public static byte[] Solve(byte[] inputBytes, byte[] bmpBytes)
        {


            string[] binarray = new string[inputBytes.Length];

            string binString = "";

            for (int i = 0; i < inputBytes.Length; i++) {
                string bin = Convert.ToString(inputBytes[i], 2);

                string k = "";
                if (bin.Length < 8) {
                    for(int j = 0; j < 8 - bin.Length; j++) {
                        k += "0";
                    }
                }

               binarray[i] = k + Convert.ToString(inputBytes[i], 2);
            }

            binString = string.Join("", binarray);

            string[] splitted = new string[binString.Length/2];

            for (int i = 0; i < binString.Length/2; i++) {
                splitted[i] = binString.Substring(2*i, 2);
            }


            //byte[] exampleByteArray = new byte[bmpBytes.Length];

            //for(int i = 0; i < 26; i++){
            //    exampleByteArray[i] = bmpBytes[i];
            //}

            for(int i = 26; i < 74; i++){
                //Console.WriteLine((bmpBytes[i] ^ Convert.ToByte(Convert.ToString(Convert.ToByte(splitted[i-26], 2),16),16)).ToString("X2"));
                bmpBytes[i] = Convert.ToByte((bmpBytes[i] ^ Convert.ToByte(Convert.ToString(Convert.ToByte(splitted[i-26], 2), 16),16)).ToString("X2"),16);
            }




            BitArray inputbits = new BitArray(inputBytes);
            byte[] exampleByteArray = new byte[bmpBytes.Length]; 
            return bmpBytes;
        }

        public static string getInputFromCommandLine(string[] args)
        {
            string input = "";
            if (args.Length == 1)
            {
                input = args[0]; // Gets the first string after the 'dotnet run' command
            }
            else
            {
                Console.WriteLine("Not enough or too many inputs provided after 'dotnet run' ");
            }
            return input;
        }

        static void Main(string[] args)
        {
            byte[] bmpBytes = new byte[]
            {
                0x42,0x4D,0x4C,0x00,0x00,0x00,0x00,0x00,
                0x00,0x00,0x1A,0x00,0x00,0x00,0x0C,0x00,
                0x00,0x00,0x04,0x00,0x04,0x00,0x01,0x00,0x18,0x00,
                0x00,0x00,0xFF,
                0xFF,0xFF,0xFF,
                0x00,0x00,0xFF,
                0xFF,0xFF,0xFF,
                0xFF,0xFF,0xFF,
                0x00,0x00,0x00,
                0xFF,0xFF,0xFF,
                0x00,0x00,0x00,
                0xFF,0x00,0x00,
                0xFF,0xFF,0xFF,
                0xFF,0x00,0x00,
                0xFF,0xFF,0xFF,
                0xFF,0xFF,0xFF,
                0x00,0x00,0x00,
                0xFF,0xFF,0xFF,
                0x00,0x00,0x00
            };

            string input = getInputFromCommandLine(args);


            Convert.ToByte("F8", 16); 
            string[] subs = input.Split(' ');

            byte[] sub2 = new byte[subs.Length];

            for (int i = 0; i < subs.Length; i++)
            {
               sub2[i] = Convert.ToByte(subs[i], 16);
            }

            byte[] inputBytes = new byte[10];
            byte[] solution = Solve(sub2, bmpBytes);

            Console.WriteLine(BitConverter.ToString(solution).Replace("-", " ")); 
        }

    }
}
