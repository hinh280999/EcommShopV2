namespace EcommShop.Contracts.Dtos.ProductComment
{
    public class ProductCommentInfoDto : BaseDto
    {
        public string Content { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
