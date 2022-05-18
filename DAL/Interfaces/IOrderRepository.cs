﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IOrderRepository:IRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllWithDetailsAsync();
        Task<Order> GetByIdWithDetailsAsync(Guid id);
    }
}
