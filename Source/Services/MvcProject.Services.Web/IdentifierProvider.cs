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

        public int DecodeId(string urlId)
        {
            var base64EncodedBytes = Convert.FromBase64String(urlId);
            var baseAsString = Encoding.UTF8
                .GetString(base64EncodedBytes)
                .Substring(0, base64EncodedBytes.Length - Salt.Length);
            return int.Parse(baseAsString);
        }

        public string EncodeId(int id)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(id.ToString() + Salt);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
