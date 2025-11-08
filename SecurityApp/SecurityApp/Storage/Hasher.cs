using Org.BouncyCastle.Crypto.Digests;
using System.Text;

namespace Storage
{
    public static class Hasher
    {
        public static string GenerateMd4Hash(string plainText)
        {
            var hashAlgorithm = new MD4Digest();
            byte[] textBytes = Encoding.UTF8.GetBytes(plainText);
            
            hashAlgorithm.BlockUpdate(textBytes, 0, textBytes.Length);
            
            byte[] hashBytes = new byte[hashAlgorithm.GetDigestSize()];
            hashAlgorithm.DoFinal(hashBytes, 0);
            
            return ConvertToHexString(hashBytes);
        }

        private static string ConvertToHexString(byte[] data)
        {
            var hexBuilder = new StringBuilder(data.Length * 2);
            
            foreach (byte b in data)
            {
                hexBuilder.AppendFormat("{0:x2}", b);
            }
            
            return hexBuilder.ToString();
        }
    }
}
