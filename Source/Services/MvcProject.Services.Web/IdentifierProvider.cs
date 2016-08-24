namespace MvcProject.Services.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class IdentifierProvider : IIdentifierProvider
    {
        private const string Salt = "khhdw6WDmn-sk!kj8m";

        public static int DecodeToIntStatic(string encodedId)
        {
            var base64EncodedBytes = Convert.FromBase64String(encodedId);
            var baseAsString = Encoding.UTF8
                .GetString(base64EncodedBytes)
                .Substring(0, base64EncodedBytes.Length - Salt.Length);
            return int.Parse(baseAsString);
        }

        public static string DecodeIdToStringStatic(string encodedId)
        {
            var base64EncodedBytes = Convert.FromBase64String(encodedId);
            var baseAsString = Encoding.UTF8
                .GetString(base64EncodedBytes)
                .Substring(0, base64EncodedBytes.Length - Salt.Length);
            return baseAsString;
        }

        public static string EncodeIntIdStatic(int id)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(id.ToString() + Salt);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string EncodeStringIdStatic(string id)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(id + Salt);
            return Convert.ToBase64String(plainTextBytes);
        }

        public int DecodeIdToInt(string encodedId)
        {
            return DecodeToIntStatic(encodedId);
        }

        public string DecodeIdToString(string encodedId)
        {
            return DecodeIdToStringStatic(encodedId);
        }

        public string EncodeIntId(int id)
        {
            return EncodeIntIdStatic(id);
        }

        public string EncodeStringId(string id)
        {
            return EncodeStringIdStatic(id);
        }
    }
}
