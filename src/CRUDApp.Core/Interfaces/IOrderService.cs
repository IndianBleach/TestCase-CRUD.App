using CRUDApp.Core.DTOs.FilterDTOs;
using CRUDApp.Core.DTOs.OrderDTOs;
using CRUDApp.Core.DTOs.ProviderDTOs;

namespace CRUDApp.Core.Interfaces
{
    public interface IOrderService
    {
        Task<List<ProviderDto>> GetAllOrderProvidersAsync();
        Task<OrderDetailDto?> GetOrderDetailOrNullAsync(int? orderId);        
        Task<string> CreateAsync(CreateOrderDto dtoModel);
        Task<List<OrderDto>> GetOrdersAsync(SearchOrdersFilterDto filter);        
        Task<bool> DeleteAsync(DeleteOrderDto dtoModel);        
        Task<bool> UpdateAsync(UpdateOrderDto dtoModel);
    }
}
