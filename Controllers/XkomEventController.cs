using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xkomEventsAPI.Data;
using xkomEventsAPI.Models;

namespace xkomEventsAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class XkomEventController : ControllerBase
    {
        private readonly XkomEventsContext _context;

        private readonly ILogger<XkomEventController> _logger;

        public XkomEventController(XkomEventsContext context, ILogger<XkomEventController> logger)
        {
            _context = context;
            _logger = logger;
            
        }

        [HttpGet]
        public IEnumerable<XkomEvent> GetAllEvents() => _context.Events.ToArray();

        [HttpGet("{eventId}", Name = "GetEventById")]
        public IActionResult GetEventById(int eventId)
        {
            var xkomEvent = _context.Events.FirstOrDefault(e => e.Id == eventId);
            return xkomEvent != null ? Ok(xkomEvent) : NotFound();
        }

        [HttpGet("{eventId}", Name = "GetEventParticipants")]
        public IActionResult GetEventParticipants(int eventId)
        {
            var participants = _context.ParticipantsToXkomEvents.Where(p => p.XkomEventId == eventId).Select(p => p.ParticipantId);
            return participants.Any() ? Ok(participants) : Ok("No participants");
        }

        [HttpPost]
        public IActionResult CreateEvent(XkomEvent xkomEvent)
        {
            if (xkomEvent == null) return BadRequest();
            if (!Enum.IsDefined(typeof(EventType), xkomEvent.EventType)) return BadRequest("Wrong type selected");

            var isEventExists = _context.Events.Any(
                e => e.EventName == xkomEvent.EventName
                && e.EventType == xkomEvent.EventType
                && e.Date == xkomEvent.Date);

            if (!isEventExists)
            {
                _context.Events.Add(xkomEvent);
                _context.SaveChanges();
                return CreatedAtRoute(nameof(GetEventById), new { eventId = xkomEvent.Id }, xkomEvent);
            }

            return Conflict($"The event \"{xkomEvent.EventName}\" (type: {xkomEvent.EventType}, date: {xkomEvent.Date}) has already been added");

        }

        [HttpDelete("{eventId}")]
        public IActionResult DeleteEvent(int? eventId)
        {
            var xkomEvent = _context.Events.FirstOrDefault(e => e.Id == eventId);
            if (xkomEvent == null) BadRequest();

            _context.Events.Remove(xkomEvent);
            _context.SaveChanges();
            return Ok(xkomEvent);

        }

        [HttpPost("{eventId}")]
        public IActionResult AddParticipant(int? eventId, Participant part)
        {
            
            var xkomEvent = _context.Events.FirstOrDefault(e => e.Id == eventId);

            if (xkomEvent == null) 
                return NotFound("Event not found");

            int xkomEventParticipantsCount = _context.ParticipantsToXkomEvents.Count(e => e.XkomEventId == eventId);

            if (xkomEvent.EventParticipantsLimit <= xkomEventParticipantsCount) 
                return BadRequest($"Too many participants. Limit = {xkomEvent.EventParticipantsLimit}");

            bool isParticipantExists = IsParticipantExists(part);

            if (!isParticipantExists)
            {
                var result = IsEmailValid(part.Email);
                
                if (result.GetType() == typeof(OkResult))
                {
                    _context.Participants.Add(part);
                    _context.SaveChanges();
                }
                else
                {
                    return result;
                }
                
            }

            var participant = _context.Participants.FirstOrDefault(p => p.Email == part.Email);

            var isParticipantAdded = _context.ParticipantsToXkomEvents.Any(p => p.ParticipantId == participant.Id);

            if (!isParticipantAdded)
            {
                var entity = new ParticipantToXkomEvent()
                {
                    ParticipantId = participant.Id,
                    XkomEventId = xkomEvent.Id
                };

                _context.ParticipantsToXkomEvents.Add(entity);
                _context.SaveChanges();

                return CreatedAtRoute(nameof(GetEventParticipants), new { eventId = entity.XkomEventId }, entity);
            }

            return Conflict($"The participant {participant.Name} ({participant.Email}) has already been added to the event");

            

        }

        private IActionResult IsEmailValid(string email)
        {
            if (!EmailValidator.IsValidEmail(email))
            {
                return BadRequest("Invalid email format. Try example@domain.com etc.");
            }
            return Ok();

        }

        private bool IsParticipantExists(Participant participant)
        {
            return _context.Participants.Any(p => p.Email == participant.Email);
        }

        private bool IsCorrectEnumValue(Type type, int value)
        {
            return Enum.IsDefined(type, value);
        }
    }
}
