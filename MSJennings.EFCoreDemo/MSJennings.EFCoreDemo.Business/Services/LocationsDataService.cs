using Microsoft.EntityFrameworkCore;
using MSJennings.EFCoreDemo.Business.DataAccess;
using MSJennings.EFCoreDemo.Business.Models;
using MSJennings.EFCoreDemo.Business.Services.Interfaces;
using System;
using System.Linq;

namespace MSJennings.EFCoreDemo.Business.Services
{
    public class LocationsDataService : ILocationsDataService
    {
        private readonly AppDbContext _context;

        public LocationsDataService(AppDbContext context)
        {
            _context = context;
        }

        public Location GetLocation(int id)
        {
            return _context.Locations.AsNoTracking().SingleOrDefault(x => x.Id == id);
        }

        public void AddLocation(Location location)
        {
            if (location is null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            _context.Locations.Add(location);
        }

        public void UpdateLocation(Location location)
        {
            if (location is null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            _context.Locations.Update(location);
        }

        public void DeleteLocation(int id)
        {
            var location = GetLocation(id);
            if (location != null)
            {
                _context.Locations.Remove(location);
                _context.SaveChanges();
            }
        }
    }
}
