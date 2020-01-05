using MSJennings.EFCoreDemo.Business.Models;
using System.Linq;

namespace MSJennings.EFCoreDemo.Business.Services.Interfaces
{
    public interface IEventsDataService
    {
        IQueryable<Event> GetEvents();

        Event GetEvent(int id);

        void AddEvent(Event @event);

        void UpdateEvent(Event @event);

        void DeleteEvent(int id);
    }
}
