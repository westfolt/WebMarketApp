﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity: BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Guid id);
        Task AddAsync(TEntity entity);
        void Delete(TEntity entity);
        Task DeleteByIdAsync(Guid id);
        void Update(TEntity entity);
    }
}
