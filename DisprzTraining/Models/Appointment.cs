using System.ComponentModel.DataAnnotations;

namespace DisprzTraining.Models
{
    public class Appointment
    {

        public Guid Id { get; set; }
       [Required(AllowEmptyStrings =false)]
        public string Date { get; set; }
        [Required(AllowEmptyStrings =false)]
        public string Title { get; set; }

        public string? Description { get; set; }
        
        [Required(AllowEmptyStrings =false)]
        public DateTime StartTime { get; set; }
         [Required(AllowEmptyStrings =false)]
        public DateTime EndTime { get; set; }

    }
}
