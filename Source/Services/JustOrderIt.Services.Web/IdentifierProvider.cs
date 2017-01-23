namespace JustOrderIt.Services.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class IdentifierProvider : IIdentifierProvider
    {
        private const string Salt = "khhdw6WDmn-sk!kj8m";

        public static int? DecodeToIntStatic(string encodedId)
        {
            if (string.IsNullOrWhiteSpace(encodedId))
            {
                return null;
            }

            var base64EncodedBytes = Convert.FromBase64String(encodedId);
            var baseAsString = Encoding.UTF8
                .GetString(base64EncodedBytes)
                .Substring(0, base64EncodedBytes.Length - Salt.Length);
            return int.Parse(baseAsString);
        }

        public static string DecodeIdToStringStatic(string encodedId)
        {
            if (string.IsNullOrWhiteSpace(encodedId))
            {
                return null;
            }

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
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            var plainTextBytes = Encoding.UTF8.GetBytes(id + Salt);
            return Convert.ToBase64String(plainTextBytes);
        }

        public int? DecodeToIntId(string encodedId)
        {
            return DecodeToIntStatic(encodedId);
        }

        public string DecodeToStringId(string encodedId)
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
