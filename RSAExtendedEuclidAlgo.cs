using System;
using System.IO;
using System.Numerics;

namespace P4
{
    class Program
    {
        static BigInteger modinv(BigInteger u, BigInteger v)
        {
           BigInteger outputInevrse, t1, t3, q;
           BigInteger y1 = 1;
           BigInteger a = u;
           BigInteger x1 = 0;
           BigInteger b = v;
           BigInteger positiveFlag = 1;
           while (b != 0)
           {
               q = a / b;
               t3 = a % b;
               t1 = y1 + q * x1;
               y1 = x1; x1 = t1; a = b; b = t3;
               positiveFlag = -positiveFlag;
           }
           if (a != 1)
               return 0;
               if (positiveFlag < 0)
                   outputInevrse = v - y1;
               else
                   outputInevrse = y1;
               return outputInevrse;
        }

        static void Main(string[] args)
        {

        /* p = 2^254-1223 = 28948022309329048855892746252171976963317496166410141009864396001978282408761
        q = 2^251-1339 = 3618502788666131106986593281521497120414687020801267626233049500247285299909
        e = 65537

        n = pq = 104748499452676539840422070298483172870932545473378073263465323779076281441762754973534368192543942213869305759932632679031823090376886792255162814102749

        d = e^-1 mod(p-1)(q-1) = 3944631225860288086518480697875344776926950001194700471706553841139512986529595573351556656469628807755318234302380127618665816722405177346937594398593

        encoding: M = m^e mod N = 66536047120374145538916787981868004206438539248910734713495276883724693574434582104900978079701174539167102706725422582788481727619546235440508214694579
        decoding: M^d mod N = 1756026041
        */


            int p_e = Int32.Parse(args[0]);

            BigInteger p_c = BigInteger.Parse(args[1]);

            BigInteger p = BigInteger.Pow(BigInteger.Parse("2"), Int32.Parse(args[0])) - BigInteger.Parse(args[1]);

            int q_e = Int32.Parse(args[2]);

            BigInteger q_c = BigInteger.Parse(args[3]);

            BigInteger q = BigInteger.Pow(BigInteger.Parse("2"), Int32.Parse(args[2])) - BigInteger.Parse(args[3]);

            BigInteger e = BigInteger.Parse("65537");

            BigInteger n = BigInteger.Multiply(p, q);

            BigInteger c = BigInteger.Multiply(BigInteger.Subtract(p, 1), BigInteger.Subtract(q, 1));

            BigInteger d = modinv(e, c);

            BigInteger decoded = BigInteger.ModPow(BigInteger.Parse(args[4]), d, n);

            BigInteger encoded = BigInteger.ModPow(BigInteger.Parse(args[5]), e, n);

            Console.WriteLine(decoded + "," + encoded);

        }
    }
}
