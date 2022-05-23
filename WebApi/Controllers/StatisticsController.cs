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
    /// Processes requests to statistic functions
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticService _statisticService;

        /// <summary>
        /// Constructor, inject statistic service here
        /// </summary>
        /// <param name="statisticService">Statistic service object</param>
        public StatisticsController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        /// <summary>
        /// Gets most popular products
        /// route: GET: api/statistic/popularProducts?productCount=2
        /// </summary>
        /// <param name="productCount">Number of products to return</param>
        /// <returns>Products list</returns>
        [HttpGet("popularProducts")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetMostPopularProducts(int productCount)
        {
            try
            {
                var products = await _statisticService.GetMostPopularProductsAsync(productCount);
                if (!products.Any())
                    return NotFound();

                return Ok(products);
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets the concrete number of most favorite products of customer
        /// route: GET api/statistic/customer/{id}/{productCount}
        /// </summary>
        /// <param name="id">Customer id to search</param>
        /// <param name="productCount">Number of products to return</param>
        /// <returns>Products list</returns>
        [HttpGet("customer/{id}/{productCount}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetCustomersMostPopularProducts(Guid id, int productCount)
        {
            try
            {
                var products = await _statisticService.GetMostPopularProductsForCustomerAsync(productCount, id);
                if (!products.Any())
                    return NotFound(id);

                return Ok(products);
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets the most active customers in a period of time, for example, from 2020-7-21 to 2020-7-22
        /// route: GET api/statistic/activity/{customerCount}?startDate= 2020-7-21&endDate= 2020-7-22
        /// </summary>
        /// <param name="customerCount">Number of customers to return</param>
        /// <param name="startDate">Start of time interval</param>
        /// <param name="endDate">End of time interval</param>
        /// <returns>Customers list</returns>
        [HttpGet("activity/{customerCount}")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetMostValuableCustomers(int customerCount, DateTime startDate, DateTime endDate)
        {
            try
            {
                var customers =
                    await _statisticService.GetMostValuableCustomersAsync(customerCount, startDate, endDate);
                if (!customers.Any())
                    return NotFound();

                return Ok(customers);
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets the income of category in a period of time, for example, from 2020-7-21 to 2020-7-22
        /// route: GET api/statistic/income/{categoryId}?startDate= 2020-7-21&endDate= 2020-7-22
        /// </summary>
        /// <param name="categoryId">Id of product category to search</param>
        /// <param name="startDate">Start of time interval</param>
        /// <param name="endDate">End of time interval</param>
        /// <returns>Calculated total sum</returns>
        [HttpGet("income/{categoryId}")]
        public async Task<ActionResult<decimal>> GetIncomeOfCategoryInPeriod(Guid categoryId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var income = await _statisticService.GetIncomeOfCategoryInPeriod(categoryId, startDate, endDate);
                if (income < 0)
                    return BadRequest($"Income is invalid: {income}");

                return Ok(income);
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
