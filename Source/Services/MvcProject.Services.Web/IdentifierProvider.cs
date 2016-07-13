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

        public int DecodeIdToInt(string urlId)
        {
            var base64EncodedBytes = Convert.FromBase64String(urlId);
            var baseAsString = Encoding.UTF8
                .GetString(base64EncodedBytes)
                .Substring(0, base64EncodedBytes.Length - Salt.Length);
            return int.Parse(baseAsString);
        }

        public string DecodeIdToString(string urlId)
        {
            var base64EncodedBytes = Convert.FromBase64String(urlId);
            var baseAsString = Encoding.UTF8
                .GetString(base64EncodedBytes)
                .Substring(0, base64EncodedBytes.Length - Salt.Length);
            return baseAsString;
        }

        public string EncodeIntId(int id)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(id.ToString() + Salt);
            return Convert.ToBase64String(plainTextBytes);
        }

        public string EncodeStringId(string id)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(id + Salt);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
