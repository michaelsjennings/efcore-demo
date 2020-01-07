using System;

namespace MSJennings.EFCoreDemo.Business.Models
{
    public class Event
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public int? LocationId { get; set; }

        public virtual Location Location { get; set; }
    }
}
