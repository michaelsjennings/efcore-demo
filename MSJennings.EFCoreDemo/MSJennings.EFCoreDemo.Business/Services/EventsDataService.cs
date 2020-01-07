using Microsoft.EntityFrameworkCore;
using MSJennings.EFCoreDemo.Business.DataAccess;
using MSJennings.EFCoreDemo.Business.Models;
using MSJennings.EFCoreDemo.Business.Services.Interfaces;
using System;
using System.Linq;

namespace MSJennings.EFCoreDemo.Business.Services
{
    public class EventsDataService : IEventsDataService
    {
        private readonly AppDbContext _context;

        public EventsDataService(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Event> GetEvents()
        {
            return _context.Events.AsNoTracking();
        }

        public Event GetEvent(int id)
        {
            return _context.Events.AsNoTracking().SingleOrDefault(x => x.Id == id);
        }

        public int AddEvent(Event @event)
        {
            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            _context.Events.Add(@event);
            _context.SaveChanges();

            return @event.Id;
        }

        public void UpdateEvent(Event @event)
        {
            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            _context.Events.Update(@event);
            _context.SaveChanges();
        }

        public void DeleteEvent(int id)
        {
            var @event = GetEvent(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
                _context.SaveChanges();
            }
        }
    }
}
