namespace MvcProject.Services.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IIdentifierProvider
    {
        int? DecodeIdToInt(string urlId);

        string DecodeIdToString(string urlId);

        string EncodeIntId(int id);

        string EncodeStringId(string id);
    }
}
