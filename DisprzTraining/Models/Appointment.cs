namespace DisprzTraining.Models
{
    public class Appointment
    {
        public Guid Id {get; set;}
        public string EventName {get; set;}=string.Empty;
        public DateTime FromTime {get; set;}
        public DateTime ToTime {get; set;}
        
    }
}

