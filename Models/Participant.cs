using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using xkomEventsAPI.Data;

namespace xkomEventsAPI.Models
{
    public class Participant
    {
        public int Id { get; set; }
        [Required(ErrorMessage = CustomErrorMessages.FieldIsRequired)]
        [Display(Name = "Name")]
        [StringLength(40, ErrorMessage = CustomErrorMessages.MaxLength)]
        [RegularExpression(@"[A-Za-zżźćńółęąśŻŹĆĄŚĘŁÓŃ\-\.\'\s]*", ErrorMessage = CustomErrorMessages.NoDigit)]
        public string Name { get; set; }
        [Required(ErrorMessage = CustomErrorMessages.FieldIsRequired)]
        [EmailAddress(ErrorMessage = CustomErrorMessages.IncorrectEmailAdress)]
        [StringLength(100)]
        public string Email { get; set; }
        [JsonIgnore]
        public virtual ICollection<XkomEvent> XkomEvents { get; set; }
    }
}
