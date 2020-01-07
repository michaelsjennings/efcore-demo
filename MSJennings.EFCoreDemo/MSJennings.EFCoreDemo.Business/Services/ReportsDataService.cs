using Microsoft.EntityFrameworkCore;
using MSJennings.EFCoreDemo.Business.DataAccess;
using MSJennings.EFCoreDemo.Business.Models;
using MSJennings.EFCoreDemo.Business.Services.Interfaces;
using System;
using System.Linq;

namespace MSJennings.EFCoreDemo.Business.Services
{
    public class ReportsDataService : IReportsDataService
    {
        private readonly AppDbContext _context;

        public ReportsDataService(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Report> GetReportsByEventId(int eventId)
        {
            return _context.Reports.AsNoTracking().Where(x => x.EventId == eventId);
        }

        public Report GetReport(int id)
        {
            return _context.Reports.AsNoTracking().SingleOrDefault(x => x.Id == id);
        }

        public int AddReport(Report report)
        {
            if (report is null)
            {
                throw new ArgumentNullException(nameof(report));
            }

            _context.Reports.Add(report);
            _context.SaveChanges();

            return report.Id;
        }

        public void UpdateReport(Report report)
        {
            if (report is null)
            {
                throw new ArgumentNullException(nameof(report));
            }

            _context.Reports.Update(report);
            _context.SaveChanges();
        }

        public void DeleteReport(int id)
        {
            var report = GetReport(id);
            if (report != null)
            {
                _context.Reports.Remove(report);
                _context.SaveChanges();
            }
        }
    }
}
