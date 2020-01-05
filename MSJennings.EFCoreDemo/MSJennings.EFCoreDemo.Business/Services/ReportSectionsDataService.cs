using Microsoft.EntityFrameworkCore;
using MSJennings.EFCoreDemo.Business.DataAccess;
using MSJennings.EFCoreDemo.Business.Models;
using MSJennings.EFCoreDemo.Business.Services.Interfaces;
using System;
using System.Linq;

namespace MSJennings.EFCoreDemo.Business.Services
{
    public class ReportSectionsDataService : IReportSectionsDataService
    {
        private readonly AppDbContext _context;

        public ReportSectionsDataService(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<ReportSection> GetReportSectionsByReportId(int reportId)
        {
            return _context.ReportSections.AsNoTracking().Where(x => x.ReportId == reportId);
        }

        public ReportSection GetReportSection(int id)
        {
            return _context.ReportSections.AsNoTracking().SingleOrDefault(x => x.Id == id);
        }

        public void AddReportSection(ReportSection reportSection)
        {
            if (reportSection is null)
            {
                throw new ArgumentNullException(nameof(reportSection));
            }

            _context.ReportSections.Add(reportSection);
        }

        public void UpdateReportSection(ReportSection reportSection)
        {
            if (reportSection is null)
            {
                throw new ArgumentNullException(nameof(reportSection));
            }

            _context.ReportSections.Update(reportSection);
        }

        public void DeleteReportSection(int id)
        {
            var reportSection = GetReportSection(id);
            if (reportSection != null)
            {
                _context.ReportSections.Remove(reportSection);
                _context.SaveChanges();
            }
        }
    }
}
