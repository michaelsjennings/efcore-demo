using Microsoft.AspNetCore.Mvc;
using MSJennings.EFCoreDemo.Business.Models;
using MSJennings.EFCoreDemo.Business.Services.Interfaces;
using MSJennings.EFCoreDemo.WebApp.ApiModels.Events;
using System.Linq;
using System.Transactions;

namespace MSJennings.EFCoreDemo.WebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsApiController : ControllerBase
    {
        private readonly IEventsDataService _eventsDataService;
        private readonly ILocationsDataService _locationsDataService;
        private readonly IReportsDataService _reportsDataService;
        private readonly IReportSectionsDataService _reportSectionsDataService;

        public EventsApiController(
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

        [HttpGet]
        public IActionResult GetEvents()
        {
            var result = _eventsDataService.GetEvents().Select(
                x => new GetEventApiModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Date = x.Date,
                    LocationDescription = x.Location.Description
                });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetEvent(int id)
        {
            var result = _eventsDataService.GetEvent(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult PostEvent([FromBody]PostEventApiModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var transaction = new TransactionScope())
            {
                var @event = new Event
                {
                    Title = model.Title,
                    Date = model.Date
                };

                if (!string.IsNullOrWhiteSpace(model.LocationDescription))
                {
                    @event.LocationId = _locationsDataService.AddLocation(new Location
                    {
                        Description = model.LocationDescription
                    });
                }

                var eventId = _eventsDataService.AddEvent(@event);

                transaction.Complete();

                return CreatedAtAction(nameof(GetEvent), new { eventId });
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutEvent(int id, [FromBody]PutEventApiModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var transaction = new TransactionScope())
            {
                var @event = _eventsDataService.GetEvent(id);

                if (@event == null)
                {
                    return NotFound();
                }

                @event.Title = model.Title;
                @event.Date = model.Date;

                int? locationIdToDelete = null;

                if (!string.IsNullOrWhiteSpace(model.LocationDescription))
                {
                    if (@event.LocationId.HasValue)
                    {
                        var location = _locationsDataService.GetLocation(@event.LocationId.Value);
                        location.Description = model.LocationDescription;

                        _locationsDataService.UpdateLocation(location);
                    }
                    else
                    {
                        @event.LocationId = _locationsDataService.AddLocation(new Location
                        {
                            Description = model.LocationDescription
                        });
                    }
                }
                else
                {
                    locationIdToDelete = @event.LocationId;
                    @event.LocationId = null;
                }

                _eventsDataService.UpdateEvent(@event);

                if (locationIdToDelete.HasValue)
                {
                    _locationsDataService.DeleteLocation(locationIdToDelete.Value);
                }

                transaction.Complete();

                return Ok();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
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
                if (@event != null)
                {
                    _eventsDataService.DeleteEvent(id);

                    if (@event.LocationId.HasValue)
                    {
                        _locationsDataService.DeleteLocation(@event.LocationId.Value);
                    }
                }

                transaction.Complete();

                return Ok();
            }
        }
    }
}