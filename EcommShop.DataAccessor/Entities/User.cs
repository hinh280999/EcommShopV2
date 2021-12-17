namespace EcommShop.DataAccessor.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Addresss { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
    }
}
