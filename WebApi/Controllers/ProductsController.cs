using BLL.Dto;
using BLL.Exceptions;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    /// <summary>
    /// Processes requests to products
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        /// <summary>
        /// Constructor, inject product service here
        /// </summary>
        /// <param name="productService">Product service object</param>
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Gets all products, invoked through GetByFilter method
        /// route: GET: api/products
        /// </summary>
        /// <returns>All products list</returns>
        [NonAction]
        private async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            IEnumerable<ProductDto> products;
            try
            {
                products = await _productService.GetAllAsync();
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }

            if (!products.Any())
                return NotFound();

            return Ok(products);
        }

        /// <summary>
        /// Gets product with specified id, route: GET: api/products/{id}
        /// </summary>
        /// <param name="id">Product id to search</param>
        /// <returns>Product item</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            ProductDto product;

            try
            {
                product = await _productService.GetByIdAsync(id);
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        /// <summary>
        /// Gets product,that meets requirements, all products if without parameters
        /// route: GET: api/products?categoryId=1&minPrice=20&maxPrice=50
        /// </summary>
        /// <param name="searchFilter">Filter to search products</param>
        /// <returns>Products list</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetByFilter([FromQuery] FilterSearchDto searchFilter)
        {
            if (searchFilter == null || searchFilter.CategoryId == null)
            {
                try
                {
                    return await GetAll();
                }
                catch (WebMarketException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                IEnumerable<ProductDto> products;

                try
                {
                    products = await _productService.GetByFilterAsync(searchFilter);
                }
                catch (WebMarketException ex)
                {
                    return BadRequest(ex.Message);
                }

                if (!products.Any())
                    return NotFound();

                return Ok(products);
            }
        }

        /// <summary>
        /// Gets all categories of products, route: GET: api/products/categories
        /// </summary>
        /// <returns>Product categories list</returns>
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetAllCategories()
        {
            IEnumerable<ProductCategoryDto> categories;

            try
            {
                categories = await _productService.GetAllProductCategoriesAsync();
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }

            if (!categories.Any())
                return NotFound();

            return Ok(categories);
        }

        /// <summary>
        /// Adds new product to database, route: POST: api/products
        /// </summary>
        /// <param name="value">New product object</param>
        /// <returns>Http status code of operation</returns>
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ProductDto value)
        {
            Guid insertId;

            if (!ModelState.IsValid)
            {
                //TODO
            }
            try
            {
                insertId = await _productService.AddAsync(value);
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(insertId);
        }

        /// <summary>
        /// Adds new product category to database, route: POST: api/products/categories
        /// </summary>
        /// <param name="value">New category to add</param>
        /// <returns>Http status code of operation</returns>
        [HttpPost("categories")]
        public async Task<ActionResult> AddCategory([FromBody] ProductCategoryDto value)
        {
            Guid insertId;

            if (!ModelState.IsValid)
            {
                //TODO
            }
            try
            {
                insertId = await _productService.AddCategoryAsync(value);
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(insertId);
        }

        /// <summary>
        /// Changes existing product, route: PUT: api/products/{id}
        /// </summary>
        /// <param name="id">Product id to change</param>
        /// <param name="value">New product object, must have the same id</param>
        /// <returns>Http status code of operation</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(Guid id, [FromBody] ProductDto value)
        {
            value.Id = id;

            if (!ModelState.IsValid)
            {
                //TODO
            }
            try
            {
                await _productService.UpdateAsync(value);
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(id);
        }

        /// <summary>
        /// Changes existing product category, route: PUT: api/products/categories/{id}
        /// </summary>
        /// <param name="id">Product category id to change</param>
        /// <param name="value">New product category value, must have the same id</param>
        /// <returns>Http status code of operation</returns>
        [HttpPut("categories/{id}")]
        public async Task<ActionResult> UpdateCategory(Guid id, [FromBody] ProductCategoryDto value)
        {
            value.Id = id;

            if (!ModelState.IsValid)
            {
                //TODO
            }
            try
            {
                await _productService.UpdateCategoryAsync(value);
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(id);
        }

        /// <summary>
        /// Deletes existing product, route: DELETE: api/products/{id}
        /// </summary>
        /// <param name="id">Id of product to delete</param>
        /// <returns>Http status code of operation</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _productService.DeleteAsync(id);
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(id);
        }

        /// <summary>
        /// Deletes existing product category, route: DELETE: api/products/categories/{id}
        /// </summary>
        /// <param name="id">Product category id to delete</param>
        /// <returns>Http status code of operation</returns>
        [HttpDelete("categories/{id}")]
        public async Task<ActionResult> DeleteCategory(Guid id)
        {
            try
            {
                await _productService.DeleteCategoryAsync(id);
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(id);
        }
    }
}
