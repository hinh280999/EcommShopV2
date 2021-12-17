using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommShop.DataAccessor.Enum
{
    public enum OrderStatus
    {
        [Description("New order")]
        NEW = 0,
        [Description("Order accepted")]
        ACCEPTED = 1,
        [Description("Order Shipping")]
        SHIPPING = 2,
        [Description("Order Completed")]
        COMPLETED = 3
    }
}
