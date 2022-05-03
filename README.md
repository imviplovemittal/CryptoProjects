# CryptoProjects

# Steganography
With a hex editor, you can manually create a bitmap (.bmp) file by typing in the bytes. Using either a downloaded hex editor or one you find online, create a bitmap using these bytes:
42 4D 4C 00 00 00 00 00 00 00 1A 00 00 00 0C 00 00 00 04 00 04 00 01 00 18 00 00 00 FF FF FF FF 00 00 FF FF FF FF FF FF FF 00 00 00 FF FF FF 00 00 00 FF 00 00 FF FF FF FF 00 00 FF FF FF FF FF FF 00 00 00 FF FF FF 00 00 00
If you save your hex file as a .bmp file, you can view your image. It will only be 4x4 pixels large, so you will need to zoom in. Figure 1 shows the expected image:
 
From the command line (not from keyboard input), you will receive a string of 12 hexadecimal digits (e.g., B1 FF FF CC 98 80 09 EA 04 48 7E C9). Since our image has 16 pixels, there are 3*16 = 48 color bytes. Hiding 2 bits per byte allows us to hide 48 * 2 = 96 bits / 8 bits/byte = 12 bytes of data.
Modify your image to hide those 12 bytes in the color bytes and print out the result in the same format as the sample code does. For example, suppose the first byte is B1. In binary, this is 10110001. We hide the values using the XOR operator. The first 4 color bytes in the above image are 00 00 FF FF (you must start after the header ends — note that the header ends at
3
0x00, not 0x18). In binary, these bytes are 00000000 00000000 11111111 11111111. Taking two bits at a time, we XOR with each byte. We start with bits 10, then 11, 00, and finally 01. Thus, our 4 bytes become 00000010 00000011 11111111 11111110.

# CryptAnalysis

Given the same seed, note that the same series of numbers will be generated regardless of how many times you run your program.
For this exercise, you will break an encryption by finding the key through a weakness in the random number generation.
Scenario: Suppose you are able to access your friend’s computer and find that they created an encryption program. Their key is generated randomly, but they chose to use the current time as the seed. By looking at the file properties, you find that the program was run at some point between 7/3/2020 (July 3) 11:00:00.000 and 7/4/2020 (July 4) 11:00:00.000 (1 day later).
In this project, the plaintext and ciphertext will be passed to you as command line arguments. Your program should output the integer value used to seed the random number generator that your friend used for that plaintext and ciphertext.
Note: You will be outputting the seed, not the key. The seed will be shorter and will simplify testing and debugging. You can always get the key using the seed.


