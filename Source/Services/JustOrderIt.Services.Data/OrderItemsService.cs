namespace JustOrderIt.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using JustOrderIt.Data.DbAccessConfig.Repositories;
    using JustOrderIt.Data.Models.Orders;
    using Web;

    public class OrderItemsService : BaseDataService<OrderItem, int, IIntPKDeletableRepository<OrderItem>>, IOrderItemsService
    {
        public OrderItemsService(IIntPKDeletableRepository<OrderItem> orderItems, IIdentifierProvider idProvider)
            : base(orderItems, idProvider)
        {
        }

        public override OrderItem GetByEncodedId(string id)
        {
            var orderItem = this.Repository.GetById((int)this.IdentifierProvider.DecodeIdToInt(id));
            return orderItem;
        }

        public override OrderItem GetByEncodedIdFromNotDeleted(string id)
        {
            var orderItem = this.Repository.GetByIdFromNotDeleted((int)this.IdentifierProvider.DecodeIdToInt(id));
            return orderItem;
        }
    }
}
