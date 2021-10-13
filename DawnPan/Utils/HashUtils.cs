using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DawnPan.Utils
{
    public class HashUtils
    {
        public static readonly SHA384 SHA384_INSTANCE = SHA384.Create();
        public static byte[] GetSha(Stream input)
        {
            return SHA384_INSTANCE.ComputeHash(input);
        }

        public static byte[] GetSha(string fileName)
        {
            using var stream = File.Create(fileName);
            return GetSha(stream);
        }

        public static string GetShaString(string fileName)
        {
            return ToString(GetSha(fileName));
        }

        public static string ToString(byte[] hash)
        {
            // Merge all bytes into a string of bytes  
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
