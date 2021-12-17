namespace EcommShop.DataAccessor.Entities
{
    public class ProductRating : BaseEntity
    {
        public int RatingPoint { get; set; }
        public string UserId { get; set; }
        public int ProductID { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
