namespace MvcProject.Services.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class IdentifierProvider : IIdentifierProvider
    {
        private const string Salt = ".443222";

        public int DecodeId(string urlId)
        {
            var base64EncodedBytes = Convert.FromBase64String(urlId);
            return int.Parse(Encoding.UTF8.GetString(base64EncodedBytes));
        }

        public string EncodeId(int id)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(id.ToString() + Salt);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
