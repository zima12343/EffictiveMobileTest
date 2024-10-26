namespace DAL.Models.Orders
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public decimal OrderWeight { get; set; }
        public Guid AreaId { get; set; }
        public string AreaName { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
