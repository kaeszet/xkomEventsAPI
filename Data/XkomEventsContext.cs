using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xkomEventsAPI.Models;

namespace xkomEventsAPI.Data
{
    public class XkomEventsContext : DbContext
    {
        public XkomEventsContext(DbContextOptions<XkomEventsContext> options) : base(options)
        {
                
        }

        public DbSet<XkomEvent> Events { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<ParticipantToXkomEvent> ParticipantsToXkomEvents { get; set; }

    }
}
