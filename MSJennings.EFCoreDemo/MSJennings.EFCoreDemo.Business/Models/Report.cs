namespace MSJennings.EFCoreDemo.Business.Models
{
    public class Report
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}
