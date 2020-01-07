using System;

namespace MSJennings.EFCoreDemo.WebApp.ApiModels.Events
{
    public class GetEventApiModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public string LocationDescription { get; set; }
    }
}
