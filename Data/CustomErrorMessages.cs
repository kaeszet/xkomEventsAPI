using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xkomEventsAPI.Data
{
    public class CustomErrorMessages
    {
        public const string FieldIsRequired = "Pole \"{0}\" jest wymagane!";
        public const string IncorrectEmailAdress = "Adres e-mail posiada nieprawidłowy format!";
        public const string NoDigit = "{0} nie może zawierać cyfr";
        public const string MaxLength = "Pole \"{0}\" posiada limit {1} znaków";
    }
}
