using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Recruitment.Functions
{
    public class HashGenerator : IHashGenerator
    {
        public async Task<string> GenerateAsync(string textToHash)
        {
            return await Task.Run(() =>
             {
                 if (string.IsNullOrEmpty(textToHash)) throw new NullReferenceException("Null string received for hashing.");

                 using var md5 = MD5.Create(textToHash);

                 var valuesBytes = md5.ComputeHash(Encoding.Unicode.GetBytes(textToHash));
                 var stringBuilder = new StringBuilder();
                 foreach (var value in valuesBytes)
                 {
                     stringBuilder.Append(value.ToString("x2"));
                 }

                 return stringBuilder.ToString();
             });
        }
    }
}