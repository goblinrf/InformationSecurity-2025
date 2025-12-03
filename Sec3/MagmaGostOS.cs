using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magma
{
	public static class MagmaGostOS
	{
		private static readonly byte[] IV = new byte[8] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };

		public static byte[] DeriveKeyFromPassword(string password)
		{
			byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
			byte[] key = new byte[32];

			for (int i = 0; i < 32; i++)
			{
				key[i] = passwordBytes[i % passwordBytes.Length];
			}

			return key;
		}

		public static void Encrypt(string inputFile, string outputFile, string password)
		{
			var key = DeriveKeyFromPassword(password);
			ProcessFileEncrypt(inputFile, outputFile, key);
		}

		public static void Decrypt(string inputFile, string outputFile, string password)
		{
			var key = DeriveKeyFromPassword(password);
			ProcessFileDecrypt(inputFile, outputFile, key);
		}

		private static void ProcessFileEncrypt(string inputFile, string outputFile, byte[] key)
		{
			if (key.Length != 32) throw new ArgumentException("Key must be 256-bit (32 bytes)");

			using var inputStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read);
			using var outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write);

			byte[] state = (byte[])IV.Clone();
			byte[] buffer = new byte[8];

			int bytesRead;
			while ((bytesRead = inputStream.Read(buffer, 0, 8)) > 0)
			{
				// prepare block (if bytesRead < 8, remaining bytes are zeros — we will write only bytesRead)
				byte[] block = new byte[8];
				Array.Clear(block, 0, 8);
				Array.Copy(buffer, 0, block, 0, bytesRead);

				byte[] gamma = MagmaEncryptBlock(state, key);

				byte[] cipherBlock = new byte[8];
				for (int j = 0; j < bytesRead; j++)
					cipherBlock[j] = (byte)(block[j] ^ gamma[j]);

				// write exactly bytesRead (so partial last block preserved)
				outputStream.Write(cipherBlock, 0, bytesRead);

				// IMPORTANT: update state = ciphertext block (not plaintext)
				Array.Copy(cipherBlock, 0, state, 0, 8);
			}
		}

		private static void ProcessFileDecrypt(string inputFile, string outputFile, byte[] key)
		{
			if (key.Length != 32) throw new ArgumentException("Key must be 256-bit (32 bytes)");

			using var inputStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read);
			using var outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write);

			byte[] state = (byte[])IV.Clone();
			byte[] buffer = new byte[8];

			int bytesRead;
			while ((bytesRead = inputStream.Read(buffer, 0, 8)) > 0)
			{
				// ciphertext block read
				byte[] cipherBlock = new byte[8];
				Array.Clear(cipherBlock, 0, 8);
				Array.Copy(buffer, 0, cipherBlock, 0, bytesRead);

				byte[] gamma = MagmaEncryptBlock(state, key);

				byte[] plainBlock = new byte[8];
				for (int j = 0; j < bytesRead; j++)
					plainBlock[j] = (byte)(cipherBlock[j] ^ gamma[j]);

				// write exactly bytesRead
				outputStream.Write(plainBlock, 0, bytesRead);

				// IMPORTANT: update state = ciphertext block (the data we just read)
				Array.Copy(cipherBlock, 0, state, 0, 8);
			}
		}


		private static readonly byte[,] SBox =
		{
			{12,4,6,2,10,5,11,9,14,8,13,7,0,3,15,1},
			{6,8,2,3,9,10,5,12,1,14,4,7,11,15,13,0},
			{11,3,5,8,2,15,10,13,14,1,7,4,12,9,6,0},
			{12,8,2,1,13,4,10,7,3,15,5,6,0,9,11,14},
			{7,15,5,10,8,1,6,13,0,9,3,14,11,4,2,12},
			{5,13,15,6,9,2,12,10,11,7,8,1,4,3,14,0},
			{8,14,2,5,6,9,1,12,15,4,11,0,13,10,3,7},
			{1,7,14,13,0,5,8,3,4,15,10,6,9,12,11,2}
		};

		private static uint[] ExpandKey(byte[] key)
		{
			uint[] subkeys = new uint[8];
			for (int i = 0; i < 8; i++)
			{
				subkeys[i] = BitConverter.ToUInt32(key, i * 4);
			}
			return subkeys;
		}

		private static byte[] MagmaEncryptBlock(byte[] block, byte[] key)
		{
			uint n1 = BitConverter.ToUInt32(block, 0);
			uint n2 = BitConverter.ToUInt32(block, 4);
			uint[] k = ExpandKey(key);


			for (int round = 0; round < 24; round++)
			{
				uint tmp = n1;
				n1 = n2 ^ F((tmp + k[round % 8]) & 0xFFFFFFFF);
				n2 = tmp;
			}


			for (int i = 7; i >= 0; i--)
			{
				uint tmp = n1;
				n1 = n2 ^ F((tmp + k[i]) & 0xFFFFFFFF);
				n2 = tmp;
			}


			byte[] result = new byte[8];
			Array.Copy(BitConverter.GetBytes(n2), 0, result, 0, 4);
			Array.Copy(BitConverter.GetBytes(n1), 0, result, 4, 4);
			return result;
		}

		private static uint F(uint x)
		{
			uint res = 0;
			for (int i = 0; i < 8; i++)
			{
				byte nibble = (byte)((x >> (i * 4)) & 0xF);
				res |= (uint)(SBox[7 - i, nibble] << (i * 4));
			}
			return (res << 11) | (res >> (32 - 11));
		}
	}
}
