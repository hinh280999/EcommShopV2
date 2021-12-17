using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommShop.Contracts.Models
{
    public class PageList<T>
    {
        public int MaxPage { get; set; }
        public int CurrentPage { get; set; }
        public List<T> ListItem { get; set; }
    }
}
