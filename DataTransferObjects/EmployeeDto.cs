using System;
using System.Linq;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace JoergIsAGeek.Workshop.DataTransferObjects
{
    public class EmployeeDto
    {
        [JsonRequired]
        [Required]
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("en")]
        [StringLength(10)]
        public string EmployeeNumber { get; set; }

        [JsonProperty("ri")]
        public int? RoomId { get; set; }

        [JsonProperty("n")]
        [StringLength(25)]
        public string Name { get; set; }

        [JsonProperty("pn")]
        [RegularExpression(@"(\d{1-6})-\d{4-10}")]
        public string PhoneNumber { get; set; }

        [JsonProperty("hp")]
        public bool HasProjects { get; set; }


        public bool IsValid(){
            var props = GetType().GetProperties();
            foreach(var prop in props){
                var vas = prop.GetCustomAttributes(typeof(ValidationAttribute), true).Cast<ValidationAttribute>();
                var val = prop.GetValue(this);
                var isvalid = vas.All(v => v.IsValid(val));
                if (!isvalid){
                    return false;
                }
            }
            return true;
        }

    }
}
