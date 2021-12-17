namespace EcommShop.DataAccessor.Entities
{
    public class ProductComment : BaseEntity
    {
        public string Content { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }


    }
}
