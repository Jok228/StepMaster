namespace StepMaster.Models.API.Day
{
    public class DayCreate
    {    
        public double calories { get; set; }
        public double distance { get; set; }
        public int steps { get; set; }
        public double plancalories { get; set; }
        public double plandistance { get; set; }
        public int plansteps { get; set; }       
        public Domain.Entity.Main.Day ConvertToBase()
        {
            return new Domain.Entity.Main.Day()
            {
                calories = this.calories,
                distance = this.distance,
                steps = this.steps,
                plancalories = this.plancalories,
                plandistance = this.plandistance,
                plansteps = this.plansteps,
            };
            
        }
    }
}
