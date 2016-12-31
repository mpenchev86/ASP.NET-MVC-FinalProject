namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MvcProject.Data.DbAccessConfig.Repositories;
    using MvcProject.Data.Models.Orders;
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
