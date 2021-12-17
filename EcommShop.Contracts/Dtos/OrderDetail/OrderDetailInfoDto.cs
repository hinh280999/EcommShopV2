namespace EcommShop.Contracts.Dtos.OrderDetail
{
    public class OrderDetailInfoDto : BaseDto
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
    }
}
