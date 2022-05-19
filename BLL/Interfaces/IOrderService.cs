using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Dto;

namespace BLL.Interfaces
{
    public interface IOrderService:ICrud<OrderDto>
    {
        Task AddProductAsync(Guid productId, Guid orderId, int quantity);
        Task DeleteProductAsync(Guid productId, Guid orderId, int quantity);
        Task<IEnumerable<OrderDetailDto>> GetOrderDetailsAsync(Guid orderId);
        Task<decimal> TotalToPay(Guid orderId);
        Task<bool> ChangeOrderStatus(Guid orderId, OrderStatusDto newStatus);
        Task<IEnumerable<OrderDto>> GetOrdersForPeriodAsync(DateTime startDate, DateTime endDate);
    }
}
