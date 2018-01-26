using System.ComponentModel.DataAnnotations;
using SharedValidation.Ui;

namespace JoergIsAGeek.Workshop.ViewModels
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