namespace MvcProject.Services.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICacheService
    {
        T Get<T>(string itemName, Func<T> dataFunc, int absoluteExpiration = 0, bool hasUpdateCallback = false, int updateAbsoluteExp = 0);

        void Remove(string itemName);
    }
}
