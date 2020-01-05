using MSJennings.EFCoreDemo.Business.Models;
using System.Linq;

namespace MSJennings.EFCoreDemo.Business.Services.Interfaces
{
    public interface IReportsDataService
    {
        IQueryable<Report> GetReportsByEventId(int eventId);

        Report GetReport(int id);

        void AddReport(Report report);

        void UpdateReport(Report report);

        void DeleteReport(int id);
    }
}
