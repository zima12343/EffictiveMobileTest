namespace DataAccess.Models
{
    public class Log
    {
        public Guid Id { get; set; }
        public string TypeLog { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
    }
}
