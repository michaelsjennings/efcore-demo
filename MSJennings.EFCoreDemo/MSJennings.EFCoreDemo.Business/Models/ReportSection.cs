namespace MSJennings.EFCoreDemo.Business.Models
{
    public class ReportSection
    {
        public int Id { get; set; }

        public int ReportId { get; set; }

        public virtual Report Report { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }
    }
}
