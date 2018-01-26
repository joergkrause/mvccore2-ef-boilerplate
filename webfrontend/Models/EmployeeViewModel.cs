using System.ComponentModel.DataAnnotations;
using SharedValidation.Ui;

namespace SodgeIt.Workshop.WebFrontEnd.Models
{
    public class EmployeeViewModel
    {

        [Hidden]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Angesteller", Description = "Name des Angestellten")]
        [StringLength(10)]
        [Required]
        //[AdditionalMetadata()]
        public string Name { get; set; }

        [StringLength(20)]
        [RegularExpression(@"(\d{1-6})-\d{4-10}")]
        [UIHint("PhoneNumber")]
        public string PhoneNumber { get; set; }

    }
}