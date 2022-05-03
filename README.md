# CryptoProjects

# Steganography
With a hex editor, you can manually create a bitmap (.bmp) file by typing in the bytes. Using either a downloaded hex editor or one you find online, create a bitmap using these bytes:
42 4D 4C 00 00 00 00 00 00 00 1A 00 00 00 0C 00 00 00 04 00 04 00 01 00 18 00 00 00 FF FF FF FF 00 00 FF FF FF FF FF FF FF 00 00 00 FF FF FF 00 00 00 FF 00 00 FF FF FF FF 00 00 FF FF FF FF FF FF 00 00 00 FF FF FF 00 00 00
If you save your hex file as a .bmp file, you can view your image. It will only be 4x4 pixels large, so you will need to zoom in. Figure 1 shows the expected image:

<img width="341" alt="Screenshot 2022-05-03 at 9 29 18 PM" src="https://user-images.githubusercontent.com/29403707/166490356-4d6c7130-af43-4aa6-abfa-ebc769e9ceac.png">
 
From the command line (not from keyboard input), you will receive a string of 12 hexadecimal digits (e.g., B1 FF FF CC 98 80 09 EA 04 48 7E C9). Since our image has 16 pixels, there are 3*16 = 48 color bytes. Hiding 2 bits per byte allows us to hide 48 * 2 = 96 bits / 8 bits/byte = 12 bytes of data.
Modify your image to hide those 12 bytes in the color bytes and print out the result in the same format as the sample code does. For example, suppose the first byte is B1. In binary, this is 10110001. We hide the values using the XOR operator. The first 4 color bytes in the above image are 00 00 FF FF (you must start after the header ends — note that the header ends at
3
0x00, not 0x18). In binary, these bytes are 00000000 00000000 11111111 11111111. Taking two bits at a time, we XOR with each byte. We start with bits 10, then 11, 00, and finally 01. Thus, our 4 bytes become 00000010 00000011 11111111 11111110.

Figure 2 shows what this bitmap looks like. Despite hiding a secret message in it, the change is impossible to see:
<img width="346" alt="Screenshot 2022-05-03 at 9 29 56 PM" src="https://user-images.githubusercontent.com/29403707/166490487-6901d37c-2e8b-4f65-aa48-f170a2944f63.png">


# CryptAnalysis (GuessSeed)

Given the same seed, note that the same series of numbers will be generated regardless of how many times you run your program.
For this exercise, you will break an encryption by finding the key through a weakness in the random number generation.
Scenario: Suppose you are able to access your friend’s computer and find that they created an encryption program. Their key is generated randomly, but they chose to use the current time as the seed. By looking at the file properties, you find that the program was run at some point between 7/3/2020 (July 3) 11:00:00.000 and 7/4/2020 (July 4) 11:00:00.000 (1 day later).
In this project, the plaintext and ciphertext will be passed to you as command line arguments. Your program should output the integer value used to seed the random number generator that your friend used for that plaintext and ciphertext.
Note: You will be outputting the seed, not the key. The seed will be shorter and will simplify testing and debugging. You can always get the key using the seed.

# Birthday Attack - Hashing (GuessPassword)
In this exercise, you will employ MD5, which is an outdated hashing algorithm. The goal of this exercise is to find collisions using a birthday attack. As the logic is the same, you will be evaluated using a hashing function with a smaller output space.
First, write a program in C# to find the MD5 hash of a string. Documentation for MD5 in C# is available here: ​MD5 Class (System.Security.Cryptography)
To convert a string to a byte array, use the Encoding.UTF8.GetBytes method. You can test your code using an online MD5 calculator. Strings converted using that method will produce identical hashes as an MD5 calculator.
A 1 byte salt will be passed to you as a command line argument. You should append this byte to the end of your byte array before hashing.
For example, suppose you are hashing the string “Hello World!” and are passed the salt C5. Bytes before hashing: 48 65 6C 6C 6F 20 57 6F 72 6C 64 21 C5
Bytes after hashing: E6 D9 B0 B9 D1 78 B2 40 02 89 EB EA 33 E8 B8 82
You can compare this result to hashing only “Hello World!” to see the difference:
Bytes before hashing (only “Hello World!”): 48 65 6C 6C 6F 20 57 6F 72 6C 64 21 Bytes after hashing: ED 07 62 87 53 2E 86 36 5E 84 1E 92 BF C5 0D 8C.
You will now perform a birthday attack with a function for calculating a hash with salt. As the standard version of MD5 is time-consuming to attack, you should modify the hashing result to
 2
make it easier to attack. Instead of using the full hash, only compare the first 5 bytes (in this case, “Hello World!” with the salt C5 hashes to E6 D9 B0 B9 D1).
You may choose the length of the string, but you may only use alphanumeric characters in your string [A-Z][a-z][0-9]. Your program should output two strings that hash to the same value with MD5 and the given salt. These strings should be separated by a comma.

# Diffie Hellman Key Exchange + AES
The first part of this assignment involves the creation of a 256-bit key for performing encryption. You will be implementing the Diffie-Hellman key exchange protocol. Typically, this protocol involves two parties concurrently sharing values and generating a key. In this assignment, you will be given all necessary values immediately and will not be required to send values over any channel. In this way, you can perform calculations as a single party.

The encryption algorithm you will be using in this project is AES (i.e., Rijndael encryption). You can use the AES class in the C# System.Security.Cryptography namespace. You will be using the 256-bit key mode. In order to perform the encryption, you will also need an IV. In this exercise, you will employ a 128-bit IV passed via command line in hex. Here is the list of values you will receive from the command line arguments, in order:
1) 128-bit IV in hex
2) g_e in base 10
3) g_c in base 10
4) N_e in base 10
5) N_c in base 10
6) xinbase10
7) gy​​ modNinbase10
8) An encrypted message C in hex
9) A plaintext message P as a string

After calculating the key, your program must perform a decryption of the given ciphertext bytes and an encryption of the given plaintext string. Your program should output these values as a comma separated pair (the decrypted text followed by the encrypted bytes).

# RSA with Extended Euclidean Algorithm to find mod inverse
you will be given prime numbers in the form (e, c). To calculate the value of the prime number, compute 2e​ ​ - c. To implement the RSA algorithm, you will need several values. These values are prime numbers p and q, e coprime to phi(n), and d such that e*d mod phi(n) = 1.
To encrypt with RSA, you need a message m and the values e and n. To decrypt, you need the encrypted message and the values d and n. In this assignment, you are required to perform both encryption and decryption.
You will be given command line arguments in the following order:
1) p_e in base 10
2) p_c in base 10
3) q_e in base 10
4) q_c in base 10
5) Ciphertext in base 10
6) Plaintext message in base 10
The value e will not be given in the command line arguments. Rather, you will use the value 21​ 6​+1 = 65,537. As mentioned before, you can calculate p = 2p​ _e​ - p_c. You can calculate q in the same way using q_e and q_c.
After calculating the value of d, you can verify that it is correct using the equation e*d mod phi(n) = 1. If this equation does not evaluate correctly, your value for d is incorrect.
Once you have calculated all of these values, decrypt the ciphertext given to you and encrypt the given plaintext. Your program should output these two values as a comma-separated pair, with the decrypted plaintext first.


