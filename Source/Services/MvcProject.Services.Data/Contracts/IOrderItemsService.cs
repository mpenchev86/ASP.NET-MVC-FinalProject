namespace MvcProject.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MvcProject.Data.Models.Orders;

    public interface IOrderItemsService : IDeletableEntitiesBaseService<OrderItem, int>
    {
    }
}
