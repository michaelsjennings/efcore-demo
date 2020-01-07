using Microsoft.AspNetCore.Mvc;
using MSJennings.EFCoreDemo.Business.Services.Interfaces;
using MSJennings.EFCoreDemo.WebApp.ViewModels.Events;
using System;
using System.Linq;
using System.Transactions;

namespace MSJennings.EFCoreDemo.WebApp.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventsDataService _eventsDataService;
        private readonly ILocationsDataService _locationsDataService;
        private readonly IReportsDataService _reportsDataService;
        private readonly IReportSectionsDataService _reportSectionsDataService;

        public EventsController(
            IEventsDataService eventsDataService,
            ILocationsDataService locationsDataService,
            IReportsDataService reportsDataService,
            IReportSectionsDataService reportSectionsDataService)
        {
            _eventsDataService = eventsDataService;
            _locationsDataService = locationsDataService;
            _reportsDataService = reportsDataService;
            _reportSectionsDataService = reportSectionsDataService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();

            viewModel.EventsList.AddRange(
                _eventsDataService.GetEvents().Select(
                    x => new EventsListItem
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Date = x.Date,
                        LocationDescription = x.Location.Description
                    }));

            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            throw new NotImplementedException();
        }

        public IActionResult Delete(int id)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    foreach (var report in _reportsDataService.GetReportsByEventId(id).ToList())
                    {
                        foreach (var reportSection in _reportSectionsDataService.GetReportSectionsByReportId(report.Id).ToList())
                        {
                            _reportSectionsDataService.DeleteReportSection(reportSection.Id);
                        }

                        _reportsDataService.DeleteReport(report.Id);
                    }

                    var @event = _eventsDataService.GetEvent(id);

                    _eventsDataService.DeleteEvent(id);

                    if (@event.LocationId.HasValue)
                    {
                        _locationsDataService.DeleteLocation(@event.LocationId.Value);
                    }

                    //
                    // un-comment the line below to simulate an unexpected error
                    // occurring during the course of the transaction
                    // this should result in the entire transaction being rolled-back
                    // and nothing should actually be deleted from the database
                    //
                    // throw new InvalidOperationException("Something went wrong!");
                    //

                    transaction.Complete();

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(HomeController.Error), "Home");
            }
        }
    }
}