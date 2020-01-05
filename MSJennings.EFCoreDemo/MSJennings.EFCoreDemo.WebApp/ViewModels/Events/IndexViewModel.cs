using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSJennings.EFCoreDemo.WebApp.ViewModels.Events
{
    public class IndexViewModel
    {
        public List<EventsListItem> EventsList { get; private set; }

        public IndexViewModel()
        {
            EventsList = new List<EventsListItem>();
        }
    }

    public class EventsListItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public string LocationDescription { get; set; }
    }
}
