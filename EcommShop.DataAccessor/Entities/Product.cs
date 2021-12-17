using System.Collections.Generic;

namespace EcommShop.DataAccessor.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }

        public Brand Brand { get; set; }
        public Category Category { get; set; }
        public virtual ICollection<ProductComment> ProductComments { get; set; }
        public virtual ICollection<ProductRating> ProductRatings { get; set; }
    }
}
