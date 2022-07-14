using CRUDApp.Core.DTOs.FilterDTOs;
using CRUDApp.Core.Interfaces;
using CRUDApp.Web.Models.HomeVM;
using Microsoft.AspNetCore.Mvc;

namespace CRUDApp.Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;

        public HomeController(IOrderService orderService,
            IOrderItemService orderItemService)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index(SearchOrdersFilterDto filter)
        {
            HomeViewModel viewModel = new(
                await _orderService.GetOrdersAsync(filter),
                await _orderService.GetAllOrderProvidersAsync(),
                new SearchOrdersFilterDto(),
                await _orderItemService.GetAllDistinctUnitsAsync());

            return View(viewModel);
        }
    }
}