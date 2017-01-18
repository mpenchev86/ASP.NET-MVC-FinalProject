namespace JustOrderIt.Web.Areas.Public.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class OrderHistoryViewModel
    {
        private ICollection<OrderForUserProfile> orders;

        public OrderHistoryViewModel()
        {
            this.orders = new HashSet<OrderForUserProfile>();
        }

        public ICollection<OrderForUserProfile> Orders
        {
            get { return this.orders; }
            set { this.orders = value; }
        }
    }
}