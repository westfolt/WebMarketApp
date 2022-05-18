using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Dto;

namespace BLL.Interfaces
{
    public interface IOrderService:ICrud<OrderDto>
    {
        Task AddProductAsync(int productId, int orderId, int quantity);
        Task DeleteProductAsync(int productId, int orderId, int quantity);
        Task<IEnumerable<OrderDetailDto>> GetOrderDetailsAsync(int orderId);
        Task<decimal> TotalToPay(int orderId);
        Task<bool> ChangeOrderStatus(int orderId, string newStatus);
    }
}
