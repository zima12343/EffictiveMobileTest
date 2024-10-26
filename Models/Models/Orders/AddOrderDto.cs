namespace DAL.Models.Orders
{
    public class AddOrderDto
    {
        public decimal OrderWeight { get; set; }
        public Guid AreaId { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
