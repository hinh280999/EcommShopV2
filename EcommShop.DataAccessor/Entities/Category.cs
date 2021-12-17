using System.Collections.Generic;

namespace EcommShop.DataAccessor.Entities
{
    public class Category : BaseEntity
    {
        public string categoryName { get; set; }
        public string categoryDescription { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
