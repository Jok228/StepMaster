



using static MongoDB.Driver.WriteConcern;

namespace StepMaster.Models.API.Day
{
    public class DayResponse
    {
        public string _id {  get; set; }
        public double? calories { get; set; }
        public double? distance { get; set; }
        public int? steps { get; set; }
        public double? plancalories { get; set; }
        public double? plandistance { get; set; }
        public int? plansteps { get; set; }

        public static Domain.Entity.Main.Day ConvertToBase(DayResponse newValue)
        {
           var day = new Domain.Entity.Main.Day();
            if (newValue.plansteps != null) day.plansteps = (int)newValue.plansteps;
            if (newValue.plandistance != null) day.plandistance = (double)newValue.plandistance;
            if (newValue.plancalories != null) day.plancalories = (double)newValue.plancalories;
            if (newValue.calories != null) day.calories = (double)newValue.calories;
            if (newValue.distance != null) day.distance = (double)newValue.distance;
            if (newValue.steps != null) day.steps = (int)newValue.steps;
            day._id = newValue._id;
            return day;
        }

    }
}
