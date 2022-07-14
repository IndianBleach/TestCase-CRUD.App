using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDApp.Core.DTOs.OrderItemDTOs;

namespace CRUDApp.Core.Interfaces;

public interface IOrderItemService
{
    Task<ICollection<UnitDto>> GetAllDistinctUnitsAsync();
    Task<bool> CreateOrderItemAsync(CreateOrderItemDto dtoModel);
    Task<bool> DeleteOrderItemAsync(DeleteOrderItemDto dtoModel);
    Task<bool> UpdateOrderItemAsync(UpdateOrderItemDto dtoModel);
}
