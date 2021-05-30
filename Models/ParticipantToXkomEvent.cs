using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xkomEventsAPI.Models
{
    public class ParticipantToXkomEvent
    {
        public int ParticipantToXkomEventId { get; set; }
        public int XkomEventId { get; set; }
        public virtual XkomEvent XkomEvent { get; set; }
        public int ParticipantId { get; set; }
        public virtual Participant Participant { get; set; }
    }
}
