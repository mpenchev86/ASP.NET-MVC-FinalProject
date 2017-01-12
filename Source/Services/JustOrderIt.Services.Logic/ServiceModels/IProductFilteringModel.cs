namespace JustOrderIt.Services.Logic.ServiceModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IProductFilteringModel
    {
        string Title { get; /*set;*/ }

        string ShortDescription { get; /*set;*/ }

        //int? DescriptionId { get; /*set;*/ }

        string DescriptionContent { get; /*set; */}

        IDictionary<string, string> DescriptionPropertiesNamesValues { get; /*set;*/ }

        decimal UnitPrice { get; /*set;*/ }

        decimal? ShippingPrice { get; /*set;*/ }

        double? AllTimeAverageRating { get; /*set; */}

        string SellerName { get; /*set;*/ }

        IEnumerable<string> TagNames { get; /*set;*/ }
    }
}
