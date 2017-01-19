namespace JustOrderIt.Web.Areas.Public.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Services.Web;

    public class ProductSideBarPartialViewModel : BasePublicViewModel<int>
    {
        public string EncodedId
        {
            get { return IdentifierProvider.EncodeIntIdStatic(this.Id); }
        }

        public string Title { get; set; }

        public string Category { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency)]
        public decimal? ShippingPrice { get; set; }
        
        public int QuantityInStock { get; set; }

        public bool IsInStock { get; set; }

        public string RefNumber { get; set; }

        //[UIHint("EditableRating")]
        public double Rating { get; set; }

        public string SellerName { get; set; }

        public List<SelectListItem> QuantityDropDownSelectList
        {
            get
            {
                var selectList = new List<SelectListItem>();
                for (int i = 0; i < this.QuantityInStock; i++)
                {
                    selectList.Add(new SelectListItem() { Text = (i + 1).ToString(), Value = (i + 1).ToString(), Selected = (i == 0) });
                }

                return selectList;
            }
        }
    }
}