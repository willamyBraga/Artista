using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace Artista.Shared 
{
    public static class Criptografia
    {
        public static string Key => "HvNFuRT@6NS=Z!(CV6y|(?:n-*<)H";

        public static string Encriptografar(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            value += Key;
            var passwordBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(passwordBytes);
        }

        public static string DecodificarCriptografia(string base64EncodedData)
        {
            if (string.IsNullOrEmpty(base64EncodedData)) return string.Empty;

            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            var result = Encoding.UTF8.GetString(base64EncodedBytes);
            result = result.Substring(0, result.Length - Key.Length);
            return result;
        }
    }
}