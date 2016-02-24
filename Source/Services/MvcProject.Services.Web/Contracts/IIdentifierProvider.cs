namespace MvcProject.Services.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IIdentifierProvider
    {
        int DecodeId(string urlId);

        string EncodeId(int id);
    }
}
