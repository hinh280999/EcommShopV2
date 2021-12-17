namespace EcommShop.Contracts.Dtos.Product
{
    public class ProductInfoDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
    }
}
