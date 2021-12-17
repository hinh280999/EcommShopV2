namespace EcommShop.Contracts.Dtos.ProductRating
{
    public class ProductRatingInfoDto : BaseDto
    {
        public int RatingPoint { get; set; }
        public string UserId { get; set; }
        public int ProductID { get; set; }
    }
}
