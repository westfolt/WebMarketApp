using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Dto;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Validation;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class ProductService:IProductService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            try
            {
                var takenFromDb = await _unitOfWork.ProductRepository.GetAllWithDetailsAsync();
                var mapped = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(takenFromDb);
                return mapped;
            }
            catch (Exception ex)
            {
                throw new WebMarketException(ex.Message);
            }
        }

        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            try
            {
                var takenFromDb = await _unitOfWork.ProductRepository.GetByIdWithDetailsAsync(id);
                var mapped = _mapper.Map<Product, ProductDto>(takenFromDb);
                return mapped;
            }
            catch (Exception ex)
            {
                throw new WebMarketException(ex.Message);
            }
        }

        public async Task<Guid> AddAsync(ProductDto entity)
        {
            if (!DtoValidationHelper.TryValidate(entity, out var validationErrors))
                throw new WebMarketException(validationErrors, nameof(entity));

            try
            {
                var mapped = _mapper.Map<ProductDto, Product>(entity);
                await _unitOfWork.ProductRepository.AddAsync(mapped);
                await _unitOfWork.SaveAsync();
                return mapped.Id;
            }
            catch (Exception ex)
            {
                throw new WebMarketException(ex.Message);
            }
        }

        public async Task UpdateAsync(ProductDto entity)
        {
            if (!DtoValidationHelper.TryValidate(entity, out var validationErrors))
                throw new WebMarketException(validationErrors, nameof(entity));

            try
            {
                var mapped = _mapper.Map<ProductDto, Product>(entity);
                _unitOfWork.ProductRepository.Update(mapped);
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
                await _unitOfWork.ProductRepository.DeleteByIdAsync(entityId);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new WebMarketException(ex.Message);
            }
        }

        public async Task<IEnumerable<ProductDto>> GetByFilterAsync(FilterSearchDto filterSearch)
        {
            if(filterSearch == null)
                throw new WebMarketException("Search filter is null",nameof(filterSearch));

            var takenFromDb = await GetAllAsync();
            if (filterSearch.CategoryId != null)
                takenFromDb = takenFromDb.Where(p => p.ProductCategoryId == filterSearch.CategoryId);
            if (filterSearch.MinPrice != null)
                takenFromDb = takenFromDb.Where(p => p.Price >= filterSearch.MinPrice);
            if (filterSearch.MaxPrice != null)
                takenFromDb = takenFromDb.Where(p => p.Price <= filterSearch.MaxPrice);

            return takenFromDb;
        }

        public async Task<IEnumerable<ProductCategoryDto>> GetAllProductCategoriesAsync()
        {
            try
            {
                var takenFromDb = await _unitOfWork.ProductCategoryRepository.GetAllAsync();
                var mapped = _mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryDto>>(takenFromDb);
                return mapped;
            }
            catch (Exception ex)
            {
                throw new WebMarketException(ex.Message);
            }
        }

        public async Task<Guid> AddCategoryAsync(ProductCategoryDto entity)
        {
            if (!DtoValidationHelper.TryValidate(entity, out var validationErrors))
                throw new WebMarketException(validationErrors, nameof(entity));

            try
            {
                var mapped = _mapper.Map<ProductCategoryDto, ProductCategory>(entity);
                await _unitOfWork.ProductCategoryRepository.AddAsync(mapped);
                await _unitOfWork.SaveAsync();
                return mapped.Id;
            }
            catch (Exception ex)
            {
                throw new WebMarketException(ex.Message);
            }
        }

        public async Task UpdateCategoryAsync(ProductCategoryDto entity)
        {
            if (!DtoValidationHelper.TryValidate(entity, out var validationErrors))
                throw new WebMarketException(validationErrors, nameof(entity));

            try
            {
                var mapped = _mapper.Map<ProductCategoryDto, ProductCategory>(entity);
                _unitOfWork.ProductCategoryRepository.Update(mapped);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new WebMarketException(ex.Message);
            }
        }

        public async Task DeleteCategoryAsync(Guid entityId)
        {
            try
            {
                await _unitOfWork.ProductCategoryRepository.DeleteByIdAsync(entityId);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new WebMarketException(ex.Message);
            }
        }
    }
}
