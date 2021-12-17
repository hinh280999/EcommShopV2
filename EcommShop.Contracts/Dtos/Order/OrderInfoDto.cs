using EcommShop.DataAccessor.Enum;
using System;

namespace EcommShop.Contracts.Dtos.Order
{
    public class OrderInfoDto : BaseDto
    {
        public OrderStatus Status { get; set; }
        public DateTime Date { get; set; }
        public string ShippingMethod { get; set; }
        public decimal TotalCost { get; set; }
    }
}
