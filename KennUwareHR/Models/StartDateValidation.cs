using System;
using System.ComponentModel.DataAnnotations;

namespace KennUwareHR.Models
{
    public class StartDateValidation : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            DateTime date = (DateTime)value;
            return date > DateTime.Now;
        }

    }
}
