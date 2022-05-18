using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Repositories;

namespace DAL.Data
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly WebMarketDbContext _dbContext;
        private ICustomerRepository _customerRepository;
        private IOrderRepository _orderRepository;
        private IOrderDetailRepository _orderDetailRepository;
        private IPersonRepository _personRepository;
        private IProductRepository _productRepository;
        private IProductCategoryRepository _productCategoryRepository;

        public UnitOfWork(WebMarketDbContext context)
        {
            _dbContext = context;
        }

        public ICustomerRepository CustomerRepository
        {
            get { return _customerRepository ??= new CustomerRepository(_dbContext); }
        }

        public IPersonRepository PersonRepository
        {
            get { return _personRepository ??= new PersonRepository(_dbContext); }
        }

        public IProductRepository ProductRepository
        {
            get { return _productRepository ??= new ProductRepository(_dbContext); }
        }

        public IProductCategoryRepository ProductCategoryRepository
        {
            get { return _productCategoryRepository ??= new ProductCategoryRepository(_dbContext); }
        }

        public IOrderRepository OrderRepository
        {
            get { return _orderRepository ??= new OrderRepository(_dbContext); }
        }

        public IOrderDetailRepository OrderDetailRepository
        {
            get { return _orderDetailRepository ??= new OrderDetailRepository(_dbContext); }
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
