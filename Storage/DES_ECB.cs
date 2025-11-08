using System.Security.Cryptography;

namespace Storage
{
	public static class DES_ECB
	{
		private static byte[] AddSaltToKey(byte[] key, byte[] salt)
		{
			var saltedKey = new byte[key.Length + salt.Length];
			Buffer.BlockCopy(key, 0, saltedKey, 0, key.Length);
			Buffer.BlockCopy(salt, 0, saltedKey, key.Length, salt.Length);
			return saltedKey;
		}

		public static void EncryptECB(byte[] key, string inputFile, string outputFile)
		{
			byte[] salt = new byte[8];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(salt);
			}

			byte[] input = File.ReadAllBytes(inputFile);
			byte[] output = ProcessECB(key, salt, input, true);

			byte[] result = new byte[salt.Length + output.Length];
			Buffer.BlockCopy(salt, 0, result, 0, salt.Length);
			Buffer.BlockCopy(output, 0, result, salt.Length, output.Length);

			File.WriteAllBytes(outputFile, result);
		}

		public static void DecryptECB(byte[] key, string inputFile, string outputFile)
		{
			byte[] input = File.ReadAllBytes(inputFile);

			if (input.Length < 8)
				throw new Exception("Файл слишком короткий для извлечения соли");

			byte[] salt = new byte[8];
			Buffer.BlockCopy(input, 0, salt, 0, 8);

			byte[] encryptedData = new byte[input.Length - 8];
			Buffer.BlockCopy(input, 8, encryptedData, 0, encryptedData.Length);

			byte[] output = ProcessECB(key, salt, encryptedData, false);
			File.WriteAllBytes(outputFile, output);
		}

		private static byte[] ProcessECB(byte[] key, byte[] salt, byte[] input, bool encrypt)
		{
			using var des = DES.Create();
			des.Mode = CipherMode.ECB;
			des.Padding = PaddingMode.PKCS7;

			// Используем ключ + соль
			byte[] saltedKey = AddSaltToKey(key, salt);
			// DES ключ должен быть 8 байт, так что берем первые 8 байт
			byte[] finalKey = new byte[8];
			Buffer.BlockCopy(saltedKey, 0, finalKey, 0, 8);

			des.Key = finalKey;

			using var transform = encrypt ? des.CreateEncryptor() : des.CreateDecryptor();
			return transform.TransformFinalBlock(input, 0, input.Length);
		}
	}
}
