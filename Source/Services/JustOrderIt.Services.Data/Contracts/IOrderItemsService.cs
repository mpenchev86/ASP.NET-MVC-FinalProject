﻿namespace JustOrderIt.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using JustOrderIt.Data.Models.Orders;

    public interface IOrderItemsService : IDeletableEntitiesBaseService<OrderItem, int>
    {
    }
}
