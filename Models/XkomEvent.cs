using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using xkomEventsAPI.Data;
using xkomEventsAPI.Models;

namespace xkomEventsAPI.Models
{
    public class XkomEvent
    {
        public int Id { get; set; }
        [Required(ErrorMessage = CustomErrorMessages.FieldIsRequired)]
        [Display(Name = "Event name")]
        [StringLength(100, ErrorMessage = CustomErrorMessages.MaxLength)]
        public string EventName { get; set; }
        [Required(ErrorMessage = CustomErrorMessages.FieldIsRequired)]
        [Display(Name = "Type")]
        public EventType EventType { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [JsonIgnore]
        public virtual ICollection<Participant> Participants { get; set; }
        public int EventParticipantsLimit => 25;

    }
}
