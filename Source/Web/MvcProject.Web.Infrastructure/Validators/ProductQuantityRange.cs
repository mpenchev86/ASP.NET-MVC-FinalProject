namespace MvcProject.Web.Infrastructure.Validators
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProductQuantityRange : RangeAttribute
    {
        public ProductQuantityRange(double min, double max)
            : base(min, max)
        {

        }
    }
}
