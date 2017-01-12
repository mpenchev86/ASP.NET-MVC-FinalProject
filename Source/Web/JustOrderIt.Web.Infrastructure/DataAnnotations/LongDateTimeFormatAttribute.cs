namespace JustOrderIt.Web.Infrastructure.DataAnnotations
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class LongDateTimeFormatAttribute : DisplayFormatAttribute
    {
        public LongDateTimeFormatAttribute()
        {
            this.DataFormatString = "{0:M/d/yyyy h:mm:ss tt}";
            this.ApplyFormatInEditMode = true;
        }
    }
}
