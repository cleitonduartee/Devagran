using System.Security.Cryptography;
using System.Text;

namespace DevagramCSharp.Utils
{
    public class MD5Utils
    {
        public static string GerarHashMD5(string texto) 
        {
            MD5 md5Hash = MD5.Create();
            var bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));

            StringBuilder stringBuilder = new StringBuilder();

            foreach(var hash in bytes)
            {
                stringBuilder.Append(hash.ToString());
            }
            return stringBuilder.ToString();
        }

    }
}
