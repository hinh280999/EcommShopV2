using System.Collections.Generic;

namespace EcommShop.Contracts.Models
{
    public class PageList<T>
    {
        public int MaxPage { get; set; }
        public int CurrentPage { get; set; }
        public List<T> ListItem { get; set; }
    }
}
