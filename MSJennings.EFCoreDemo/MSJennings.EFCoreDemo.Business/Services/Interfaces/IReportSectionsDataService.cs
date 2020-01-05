using MSJennings.EFCoreDemo.Business.Models;
using System.Linq;

namespace MSJennings.EFCoreDemo.Business.Services.Interfaces
{
    public interface IReportSectionsDataService
    {
        IQueryable<ReportSection> GetReportSectionsByReportId(int reportId);

        ReportSection GetReportSection(int id);

        void AddReportSection(ReportSection reportSection);

        void UpdateReportSection(ReportSection reportSection);

        void DeleteReportSection(int id);
    }
}
