using BLL.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IOrderService : ICrud<OrderDto>
    {
        Task AddProductAsync(Guid productId, Guid orderId, int quantity);
        Task DeleteProductAsync(Guid productId, Guid orderId, int quantity);
        Task<IEnumerable<OrderDetailDto>> GetOrderDetailsAsync(Guid orderId);
        Task<decimal> TotalToPay(Guid orderId);
        Task ChangeOrderStatus(Guid orderId, OrderStatusDto newStatus);
        Task<IEnumerable<OrderDto>> GetOrdersForPeriodAsync(DateTime startDate, DateTime endDate);
    }
}
