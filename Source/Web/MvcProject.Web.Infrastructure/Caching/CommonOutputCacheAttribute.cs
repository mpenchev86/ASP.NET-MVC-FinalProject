namespace MvcProject.Web.Infrastructure.Caching
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class CommonOutputCacheAttribute : OutputCacheAttribute
    {
        public CommonOutputCacheAttribute()
        {
            this.Duration = 10 * 60;
            this.VaryByParam = "none";
        }
    }
}