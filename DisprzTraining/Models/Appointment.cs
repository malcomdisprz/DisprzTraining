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
//dotnet test --collect:"XPlat Code Coverage"
//dotnet tool install -g dotnet-reportgenerator-globaltool
//reportgenerator -reports:"TestResults\{id}\coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
//replace id
