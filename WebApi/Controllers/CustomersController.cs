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
    /// Processes requests to customers
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        /// <summary>
        /// Constructor, inject customer service here
        /// </summary>
        /// <param name="customerService">Customer service object</param>
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Gets all customers list, route: GET: api/customers
        /// </summary>
        /// <returns>All customers in database</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> Get()
        {
            IEnumerable<CustomerDto> customers;
            try
            {
                customers = await _customerService.GetAllAsync();
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }

            if (!customers.Any())
                return NotFound();

            return Ok(customers);
        }

        /// <summary>
        /// Gets customer with id, route: GET: api/customers/{id}
        /// </summary>
        /// <param name="id">Customer's id to search</param>
        /// <returns>Customer with specified id, if exists</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetById(Guid id)
        {
            CustomerDto customer;
            try
            {
                customer = await _customerService.GetByIdAsync(id);
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        /// <summary>
        /// Gets all customers, who bought specified product, route: GET: api/customers/products/{id}
        /// </summary>
        /// <param name="id">product id to search</param>
        /// <returns>Customers list</returns>
        [HttpGet("products/{id}")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetByProductId(Guid id)
        {
            IEnumerable<CustomerDto> customers;
            try
            {
                customers = await _customerService.GetCustomersByProductIdAsync(id);
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }

            if (!customers.Any())
                return NotFound();

            return Ok(customers);
        }

        /// <summary>
        /// Adds new customer to database, route: POST: api/customers
        /// </summary>
        /// <param name="value">New customer entity</param>
        /// <returns>Http status code of operation</returns>
        [HttpPost]
        public async Task<ActionResult<Guid>> Add([FromBody] CustomerDto value)
        {
            Guid insertId;
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            try
            {
                insertId = await _customerService.AddAsync(value);
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(insertId);
        }

        /// <summary>
        /// Changes existing customer, route: PUT: api/customers/{id}
        /// </summary>
        /// <param name="id">Id of customer to modify</param>
        /// <param name="value">New customer value with changed parameters, must have the same id</param>
        /// <returns>Http status code of operation</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] CustomerDto value)
        {
            value.Id = id;

            if (!ModelState.IsValid)
            {
                //TODO
            }
            try
            {
                await _customerService.UpdateAsync(value);
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(id);
        }

        /// <summary>
        /// Deletes customer from database, route: DELETE: api/customers/{id}
        /// </summary>
        /// <param name="id">Id of customer to delete</param>
        /// <returns>Http status code of operation</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _customerService.DeleteAsync(id);
            }
            catch (WebMarketException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok(id);
        }
    }
}
