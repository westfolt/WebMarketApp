using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Dto;
using BLL.Interfaces;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Linq;
using BLL.Exceptions;
using BLL.Validation;
using DAL.Entities;

namespace BLL.Services
{
    public class CustomerService:ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            try
            {
                var takenFromDb = await _unitOfWork.CustomerRepository.GetAllWithDetailsAsync();
                var mapped = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(takenFromDb);
                return mapped;
            }
            catch (Exception ex)
            {
                throw new WebMarketException(ex.Message);
            }
        }

        public async Task<CustomerDto> GetByIdAsync(Guid id)
        {
            try
            {
                var takenFromDb = await _unitOfWork.CustomerRepository.GetByIdWithDetailsAsync(id);
                var mapped = _mapper.Map<Customer, CustomerDto>(takenFromDb);
                return mapped;
            }
            catch (Exception ex)
            {
                throw new WebMarketException(ex.Message);
            }
        }

        public async Task<Guid> AddAsync(CustomerDto entity)
        {
            if(!DtoValidationHelper.TryValidate(entity, out var validationErrors))
            {
                throw new WebMarketException(validationErrors, nameof(entity));
            }

            try
            {
                var mapped = _mapper.Map<CustomerDto, Customer>(entity);
                await _unitOfWork.CustomerRepository.AddAsync(mapped);
                await _unitOfWork.SaveAsync();
                return mapped.Id;
            }
            catch (Exception ex)
            {
                throw new WebMarketException(ex.Message);
            }
        }

        public async Task UpdateAsync(CustomerDto entity)
        {
            if (!DtoValidationHelper.TryValidate(entity, out var validationErrors))
            {
                throw new WebMarketException(validationErrors, nameof(entity));
            }

            try
            {
                var mapped = _mapper.Map<CustomerDto, Customer>(entity);
                _unitOfWork.CustomerRepository.Update(mapped);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new WebMarketException(ex.Message);
            }
        }

        public async Task DeleteAsync(Guid entityId)
        {
            try
            {
                await _unitOfWork.CustomerRepository.DeleteByIdAsync(entityId);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new WebMarketException(ex.Message);
            }
        }

        public async Task<IEnumerable<CustomerDto>> GetCustomersByProductAsync(Guid productId)
        {
            try
            {
                var customers = (await _unitOfWork.CustomerRepository.GetAllWithDetailsAsync()).Where(c =>
                    c.Orders.Any(o => o.OrderDetails.Any(od => od.ProductId == productId)));
                var mapped = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(customers);
                return mapped;
            }
            catch (Exception ex)
            {
                throw new WebMarketException(ex.Message);
            }
        }
    }
}
