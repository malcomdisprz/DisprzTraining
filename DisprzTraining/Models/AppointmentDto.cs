namespace DisprzTraining.Models
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}