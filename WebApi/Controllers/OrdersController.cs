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
    /// Processes requests to orders
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        /// <summary>
        /// Constructor, inject order service here
        /// </summary>
        /// <param name="orderService">Order service object</param>
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Gets all orders, route: GET: api/orders
        /// </summary>
        /// <returns>Orders list</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> Get()
        {
            IEnumerable<OrderDto> orders;

            try
            {
                orders = await _orderService.GetAllAsync();
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }

            if (!orders.Any())
                return NotFound();

            return Ok(orders);
        }

        /// <summary>
        /// Gets order with specified id, route: GET api/orders/{id}
        /// </summary>
        /// <param name="id">Id of order to search</param>
        /// <returns>Order with specified id</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(Guid id)
        {
            OrderDto order;

            try
            {
                order = await _orderService.GetByIdAsync(id);
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        /// <summary>
        /// Get details of order with specified id
        /// </summary>
        /// <param name="id">Id of order to search</param>
        /// <returns>Found order details</returns>
        // GET api/orders/{id}/details
        [HttpGet("{id}/details")]
        public async Task<ActionResult<OrderDetailDto>> GetAllDetails(Guid id)
        {
            try
            {
                var orderDetails = await _orderService.GetOrderDetailsAsync(id);

                if (!orderDetails.Any())
                    return NotFound();

                return Ok(orderDetails);
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets total cost of all products included in order
        /// route: GET api/orders/{id}/sum
        /// </summary>
        /// <param name="id">Id of order</param>
        /// <returns>Calculated sum</returns>
        [HttpGet("{id}/sum")]
        public async Task<ActionResult<decimal>> GetOrderSum(Guid id)
        {
            try
            {
                var sum = await _orderService.TotalToPay(id);

                if (sum <= 0)
                    return NotFound(id);

                return Ok(sum);
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets all orders for selected time period
        /// route: GET api/orders/period?startDate=2021-12-1&endDate=2020-12-31
        /// </summary>
        /// <param name="startDate">Start of time period</param>
        /// <param name="endDate">End of time period</param>
        /// <returns>Orders list for selected period</returns>
        [HttpGet("period")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllForPeriod(DateTime startDate, DateTime endDate)
        {
            try
            {
                var orders = await _orderService.GetOrdersForPeriodAsync(startDate, endDate);

                if (!orders.Any())
                    return NotFound();

                return Ok(orders);
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Adds order to database, route: POST api/orders
        /// </summary>
        /// <param name="value">Order object</param>
        /// <returns>Http status code of operation</returns>
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] OrderDto value)
        {
            Guid insertId;

            if (!ModelState.IsValid)
            {
                //TODO
            }
            try
            {
                insertId = await _orderService.AddAsync(value);
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(insertId);
        }

        /// <summary>
        /// Changes existing order, route: PUT api/orders/{id}
        /// </summary>
        /// <param name="id">Id of order to modify</param>
        /// <param name="value">New order object, must have the same id</param>
        /// <returns>Http status code of operation</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] OrderDto value)
        {
            value.Id = id;

            if (!ModelState.IsValid)
            {
                //TODO
            }
            try
            {
                await _orderService.UpdateAsync(value);
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(id);
        }

        /// <summary>
        /// Adds product to order, changes product amount, if such product already exists in the order
        /// route: PUT api/orders/{id}/products/add/{productId}/{quantity}
        /// </summary>
        /// <param name="id">Receipt id to modify</param>
        /// <param name="productId">Product id to add</param>
        /// <param name="quantity">Product quantity to add</param>
        /// <returns>Http status code of operation</returns>
        [HttpPut("{id}/products/add/{productId}/{quantity}")]
        public async Task<ActionResult> UpdateAddProducts(Guid id, Guid productId, int quantity)
        {
            try
            {
                await _orderService.AddProductAsync(productId, id, quantity);
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Parameter: {ex.ParamName}\nInfo: {ex.Message}");
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }
            //TODO
            return Ok($"Id:{id}, productId: {productId}, quantity: {quantity}");
        }

        /// <summary>
        /// Removes product to order, changes product amount, if new amount is 0 - deletes product from order
        /// route: PUT api/orders/{id}/products/remove/{productId}/{quantity}
        /// </summary>
        /// <param name="id">Order id to modify</param>
        /// <param name="productId">Product id to remove</param>
        /// <param name="quantity">Product quantity to substract</param>
        /// <returns>Http status code of operation</returns>
        [HttpPut("{id}/products/remove/{productId}/{quantity}")]
        public async Task<ActionResult> UpdateRemoveProducts(Guid id, Guid productId, int quantity)
        {
            try
            {
                await _orderService.DeleteProductAsync(productId, id, quantity);
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Parameter: {ex.ParamName}\nInfo: {ex.Message}");
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }
            //TODO
            return Ok($"Id:{id}, productId: {productId}, quantity: {quantity}");
        }

        /// <summary>
        /// Sets checkout status to order, route: PUT api/orders/{id}/status
        /// </summary>
        /// <param name="id">Order id to modify</param>
        /// <param name="newStatus">New order status</param>
        /// <returns>Http status of operation</returns>
        [HttpPut("{id}/status")]
        public async Task<ActionResult> UpdateOrderStatus(Guid id, [FromBody] OrderStatusDto newStatus)
        {
            try
            {
                await _orderService.ChangeOrderStatus(id, newStatus);
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok($"Set new status for id:{id}\n" +
                      $"new status: {newStatus}");
        }

        /// <summary>
        /// Deletes order from database, route: DELETE api/orders/{id}
        /// </summary>
        /// <param name="id">Order id to delete</param>
        /// <returns>Http status code of operation</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _orderService.DeleteAsync(id);
            }
            catch (WebMarketException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(id);
        }
    }
}
