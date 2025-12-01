using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Md4
{
	using System;
	using System.Text;

	public static class Md4
	{
		public static byte[] ComputeHash(byte[] message)
		{
			uint A = 0x67452301u;
			uint B = 0xEFCDAB89u;
			uint C = 0x98BADCFEu;
			uint D = 0x10325476u;

			byte[] padded = Pad(message);

			for (int offset = 0; offset < padded.Length; offset += 64)
			{
				uint[] X = new uint[16];
				for (int i = 0; i < 16; i++)
				{
					int idx = offset + i * 4;
					X[i] = (uint)(padded[idx] | (padded[idx + 1] << 8) | (padded[idx + 2] << 16) | (padded[idx + 3] << 24));
				}

				uint AA = A, BB = B, CC = C, DD = D;

				static uint F(uint x, uint y, uint z) => (x & y) | (~x & z);
				static uint G(uint x, uint y, uint z) => (x & y) | (x & z) | (y & z);
				static uint H(uint x, uint y, uint z) => x ^ y ^ z;
				static uint Rol(uint val, int s) => (val << s) | (val >> (32 - s));

				A = Rol(unchecked(A + F(B, C, D) + X[0]), 3);
				D = Rol(unchecked(D + F(A, B, C) + X[1]), 7);
				C = Rol(unchecked(C + F(D, A, B) + X[2]), 11);
				B = Rol(unchecked(B + F(C, D, A) + X[3]), 19);

				A = Rol(unchecked(A + F(B, C, D) + X[4]), 3);
				D = Rol(unchecked(D + F(A, B, C) + X[5]), 7);
				C = Rol(unchecked(C + F(D, A, B) + X[6]), 11);
				B = Rol(unchecked(B + F(C, D, A) + X[7]), 19);

				A = Rol(unchecked(A + F(B, C, D) + X[8]), 3);
				D = Rol(unchecked(D + F(A, B, C) + X[9]), 7);
				C = Rol(unchecked(C + F(D, A, B) + X[10]), 11);
				B = Rol(unchecked(B + F(C, D, A) + X[11]), 19);

				A = Rol(unchecked(A + F(B, C, D) + X[12]), 3);
				D = Rol(unchecked(D + F(A, B, C) + X[13]), 7);
				C = Rol(unchecked(C + F(D, A, B) + X[14]), 11);
				B = Rol(unchecked(B + F(C, D, A) + X[15]), 19);

				const uint K2 = 0x5A827999u;
				A = Rol(unchecked(A + G(B, C, D) + X[0] + K2), 3);
				D = Rol(unchecked(D + G(A, B, C) + X[4] + K2), 5);
				C = Rol(unchecked(C + G(D, A, B) + X[8] + K2), 9);
				B = Rol(unchecked(B + G(C, D, A) + X[12] + K2), 13);

				A = Rol(unchecked(A + G(B, C, D) + X[1] + K2), 3);
				D = Rol(unchecked(D + G(A, B, C) + X[5] + K2), 5);
				C = Rol(unchecked(C + G(D, A, B) + X[9] + K2), 9);
				B = Rol(unchecked(B + G(C, D, A) + X[13] + K2), 13);

				A = Rol(unchecked(A + G(B, C, D) + X[2] + K2), 3);
				D = Rol(unchecked(D + G(A, B, C) + X[6] + K2), 5);
				C = Rol(unchecked(C + G(D, A, B) + X[10] + K2), 9);
				B = Rol(unchecked(B + G(C, D, A) + X[14] + K2), 13);

				A = Rol(unchecked(A + G(B, C, D) + X[3] + K2), 3);
				D = Rol(unchecked(D + G(A, B, C) + X[7] + K2), 5);
				C = Rol(unchecked(C + G(D, A, B) + X[11] + K2), 9);
				B = Rol(unchecked(B + G(C, D, A) + X[15] + K2), 13);

				const uint K3 = 0x6ED9EBA1u;
				A = Rol(unchecked(A + H(B, C, D) + X[0] + K3), 3);
				D = Rol(unchecked(D + H(A, B, C) + X[8] + K3), 9);
				C = Rol(unchecked(C + H(D, A, B) + X[4] + K3), 11);
				B = Rol(unchecked(B + H(C, D, A) + X[12] + K3), 15);

				A = Rol(unchecked(A + H(B, C, D) + X[2] + K3), 3);
				D = Rol(unchecked(D + H(A, B, C) + X[10] + K3), 9);
				C = Rol(unchecked(C + H(D, A, B) + X[6] + K3), 11);
				B = Rol(unchecked(B + H(C, D, A) + X[14] + K3), 15);

				A = Rol(unchecked(A + H(B, C, D) + X[1] + K3), 3);
				D = Rol(unchecked(D + H(A, B, C) + X[9] + K3), 9);
				C = Rol(unchecked(C + H(D, A, B) + X[5] + K3), 11);
				B = Rol(unchecked(B + H(C, D, A) + X[13] + K3), 15);

				A = Rol(unchecked(A + H(B, C, D) + X[3] + K3), 3);
				D = Rol(unchecked(D + H(A, B, C) + X[11] + K3), 9);
				C = Rol(unchecked(C + H(D, A, B) + X[7] + K3), 11);
				B = Rol(unchecked(B + H(C, D, A) + X[15] + K3), 15);

				A = unchecked(A + AA);
				B = unchecked(B + BB);
				C = unchecked(C + CC);
				D = unchecked(D + DD);
			}

			byte[] digest = new byte[16];
			Array.Copy(BitConverter.GetBytes(A), 0, digest, 0, 4);
			Array.Copy(BitConverter.GetBytes(B), 0, digest, 4, 4);
			Array.Copy(BitConverter.GetBytes(C), 0, digest, 8, 4);
			Array.Copy(BitConverter.GetBytes(D), 0, digest, 12, 4);
			return digest;
		}

		private static byte[] Pad(byte[] input)
		{
			ulong bitLen = (ulong)input.Length * 8UL;

			int padLen = (56 - ((input.Length + 1) % 64) + 64) % 64;
			byte[] output = new byte[input.Length + 1 + padLen + 8];
			Array.Copy(input, 0, output, 0, input.Length);
			output[input.Length] = 0x80;

			byte[] lenBytes = BitConverter.GetBytes(bitLen);
			Array.Copy(lenBytes, 0, output, output.Length - 8, 8);
			return output;
		}
	}
}
