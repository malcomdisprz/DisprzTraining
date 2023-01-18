using DisprzTraining.Models;
namespace DisprzTraining.Data
{
    public static class DataStore
    {
        public static List<Appointment> newList = new List<Appointment>()
       {
        
       };
       public static Dictionary<DateTime,List<Appointment> > dictionaryData= new Dictionary<DateTime,List<Appointment>>();
    

    }
}