using CRUDApp.Core.DTOs.FilterDTOs;
using CRUDApp.Core.DTOs.OrderDTOs;
using CRUDApp.Core.DTOs.OrderItemDTOs;
using CRUDApp.Core.DTOs.ProviderDTOs;

namespace CRUDApp.Web.Models.HomeVM
{
    public class HomeViewModel
    {
        public ICollection<UnitDto> Units { get; set; }
        public ICollection<ProviderDto> Providers { get; set; }
        public ICollection<OrderDto> Orders { get; set; }
        public SearchOrdersFilterDto Filter { get; set; }

        public HomeViewModel(ICollection<OrderDto> orderDtos,
            ICollection<ProviderDto> providers,
            SearchOrdersFilterDto filter,
            ICollection<UnitDto> units)
        {
            Units = units;
            Filter = filter;
            Orders = orderDtos;
            Providers = providers;
        }
    }
}
