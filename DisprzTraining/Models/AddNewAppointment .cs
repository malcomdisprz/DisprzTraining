using System.ComponentModel.DataAnnotations;
namespace DisprzTraining.Models
{
    public class AddNewAppointment 
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="Please select a date")]
        public string? Date { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }

         [Required(AllowEmptyStrings =false,ErrorMessage ="Please select start-time")]
        public DateTime StartTime { get; set; }
         [Required(AllowEmptyStrings =false,ErrorMessage ="Please select end-time")]
        public DateTime EndTime { get; set; }
    }
}