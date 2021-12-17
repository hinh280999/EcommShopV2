namespace EcommShop.DataAccessor.Entities
{
    public class ProductImage : BaseEntity
    {
        public string ProductImageName { get; set; }
        public string ImagePath { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
