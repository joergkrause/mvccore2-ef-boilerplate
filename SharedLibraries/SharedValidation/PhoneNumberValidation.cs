using System;
using System.ComponentModel.DataAnnotations;

namespace JoergIsAGeek.Workshop.SharedValidation
{
    public class PhoneNumberValidation : ValidationAttribute 
    {

        public override bool IsValid(object value){
            if (value == null) return false;
            var phoneNumber = value.ToString();
            return phoneNumber.Contains("07433");
        } 

    }
}
