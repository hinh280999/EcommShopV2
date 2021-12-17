namespace EcommShop.Contracts.Dtos.ProductImage
{
    public class ProductImageInfoDto : BaseDto
    {
        public string ProductImageName { get; set; }
        public string ImagePath { get; set; }
        public int ProductId { get; set; }
    }
}
