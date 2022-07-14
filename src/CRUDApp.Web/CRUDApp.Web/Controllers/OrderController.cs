using CRUDApp.Core.DTOs.OrderDTOs;
using CRUDApp.Core.DTOs.OrderItemDTOs;
using CRUDApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRUDApp.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;

        public OrderController(IOrderService orderService,
            IOrderItemService orderItemService)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
        }

        [HttpPost]
        [Route("/order/deleteitem")]
        public async Task<JsonResult> DeleteOrderItem(DeleteOrderItemDto model)
            => Json(await _orderItemService.DeleteOrderItemAsync(model));

        [HttpPost]
        [Route("/order/edititem")]
        public async Task<JsonResult> EditOrderItem(UpdateOrderItemDto model)
            => Json(await _orderItemService.UpdateOrderItemAsync(model));

        [HttpPost]
        [Route("/order/additem")]
        public async Task<JsonResult> AddItemToOrder(CreateOrderItemDto model)
            => Json(await _orderItemService.CreateOrderItemAsync(model));

        [HttpPost]
        [Route("/order/delete")]
        public async Task<JsonResult> DeleteOrder(DeleteOrderDto model)
            => Json(await _orderService.DeleteAsync(model));

        [HttpPost]
        [Route("/order/update")]
        public async Task<JsonResult> UpdateOrder(UpdateOrderDto model)
            => Json(await _orderService.UpdateAsync(model));

        [HttpGet]
        [Route("/order/detail")]
        public async Task<JsonResult> OrderDetail(int id)
            => Json(await _orderService.GetOrderDetailOrNullAsync(id));

        [HttpPost]
        [Route("/order/create")]
        public async Task<JsonResult> CreateOrder(CreateOrderDto model)
            => Json(await _orderService.CreateAsync(model));
    }
}
