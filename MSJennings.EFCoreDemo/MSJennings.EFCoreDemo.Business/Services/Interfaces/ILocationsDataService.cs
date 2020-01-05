using MSJennings.EFCoreDemo.Business.Models;

namespace MSJennings.EFCoreDemo.Business.Services.Interfaces
{
    public interface ILocationsDataService
    {
        Location GetLocation(int id);

        void AddLocation(Location location);

        void UpdateLocation(Location location);

        void DeleteLocation(int id);
    }
}
