using EcommShop.DataAccessor.Enum;
using System;
using System.Collections.Generic;

namespace EcommShop.DataAccessor.Entities
{
    public class Order : BaseEntity
    {
        public OrderStatus Status { get; set; }
        public DateTime Date { get; set; }
        public string ShippingMethod { get; set; }
        public decimal TotalCost { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }

    }
}
