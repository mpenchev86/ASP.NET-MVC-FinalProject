namespace MvcProject.Services.Web.ServiceModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Caching;

    public class CacheSettings
    {
        public object UpdatedCacheValue { get; set; }

        public bool UpdatedValueFlag { get; set; }

        public int AbsoluteExpiration { get; set; }

        public int UpdateDelay { get; set; }

        public Delegate ExpensiveOperation { get; set; }

        public CacheItemUpdateCallback OnUpdateCallback { get; set; }
    }
}
