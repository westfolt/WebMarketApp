﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IProductCategoryRepository:IRepository<ProductCategory>
    {
        Task<IEnumerable<ProductCategory>> GetAllWithDetailsAsync();
        Task<ProductCategory> GetByIdWithDetailsAsync(Guid id);
    }
}
