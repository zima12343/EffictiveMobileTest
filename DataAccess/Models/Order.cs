namespace DataAccess.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public decimal OrderWeight { get; set; }
        public Guid AreaId { get; set; }
        public Area Area { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
