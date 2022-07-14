using AutoMapper;
using CRUDApp.Core.DTOs.OrderItemDTOs;
using CRUDApp.Core.Entities.OrderEntity;
using CRUDApp.Core.Entities.OrderItemEntity;
using CRUDApp.Core.Interfaces;
using CRUDApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDApp.Infrastructure.Services.OrderItemService
{
    public class OrderItemService : IOrderItemService
    {
        private readonly ApplicationDbContext _appContext;
        public OrderItemService(ApplicationDbContext context)
        {
            _appContext = context;
        }

        /// <summary>
        /// Добавить элемент в заказ
        /// </summary>
        /// <param name="dtoModel"></param>
        /// <returns>True, если элемент успешно добавлен, иначе - false</returns>
        public async Task<bool> CreateOrderItemAsync(CreateOrderItemDto dtoModel)
        {
            Order? getOrder = await _appContext.Orders
                .Include(x => x.OrderItems)
                .FirstOrDefaultAsync(x => x.Id.Equals(dtoModel.OrderId));

            if (getOrder != null)
            {
                try
                {
                    IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
                    decimal quantity = decimal.Parse(dtoModel.Quantity, formatter);

                    OrderItem orderItem = new(dtoModel.OrderId, dtoModel.Name, quantity, dtoModel.Unit);

                    await _appContext.OrderItems.AddAsync(orderItem);
                    await _appContext.SaveChangesAsync();

                    // -> logger
                    return true;
                }
                catch (Exception exp)
                {
                    // -> logger
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Удаление элемента из заказа
        /// </summary>
        /// <param name="dtoModel"></param>
        /// <returns>True, если операция прошла успешно, иначе - false</returns>
        public async Task<bool> DeleteOrderItemAsync(DeleteOrderItemDto dtoModel)
        {
            OrderItem? getItem = await _appContext.OrderItems
                .FirstOrDefaultAsync(x => x.Id.Equals(dtoModel.OrderItemId));

            if (getItem == null) return false;

            try
            {
                _appContext.OrderItems.Remove(getItem);
                await _appContext.SaveChangesAsync();

                // -> logger
                return true;
            }
            catch (Exception exp)
            {
                // -> logger
                return false;
            }
        }

        /// <summary>
        /// Все уникальные единицы измерения у элементов
        /// </summary>
        /// <returns>Список dto объектов единиц измерения</returns>
        public async Task<ICollection<UnitDto>> GetAllDistinctUnitsAsync()
        {
            return await _appContext.OrderItems
                .Select(x => new UnitDto()
                { 
                    Name = x.Unit
                })
                .Distinct()
                .ToListAsync();            
        }

        /// <summary>
        /// Обновить элемент заказа
        /// </summary>
        /// <param name="dtoModel"></param>
        /// <returns>True, если обновление прошло успешно, иначе - false</returns>
        public async Task<bool> UpdateOrderItemAsync(UpdateOrderItemDto dtoModel)
        {
            OrderItem? getItem = await _appContext.OrderItems
                .FirstOrDefaultAsync(x => x.OrderId.Equals(dtoModel.OrderId) &&
                x.Id.Equals(dtoModel.ItemId));

            if (getItem != null)
            {
                try
                {
                    if (!string.IsNullOrEmpty(dtoModel.Quantity))
                    {
                        IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
                        getItem.Quantity = decimal.Parse(dtoModel.Quantity, formatter);
                    }

                    if (!string.IsNullOrEmpty(dtoModel.Unit))
                        getItem.Unit = dtoModel.Unit;

                    if (!string.IsNullOrWhiteSpace(dtoModel.Name))
                        getItem.Name = dtoModel.Name;

                    _appContext.OrderItems.Update(getItem);
                    await _appContext.SaveChangesAsync();

                    // -> logger
                    return true;
                }
                catch (Exception exp)
                {
                    // -> logger
                    return false;
                }
            }

            return false;
        }
    }
}
