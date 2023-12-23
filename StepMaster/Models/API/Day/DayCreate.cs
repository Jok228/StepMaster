using System.ComponentModel.DataAnnotations;

namespace StepMaster.Models.API.Day
{
    public class DayCreate
    {
        [Required]
        public double? calories { get; set; }
        [Required]
        public double? distance { get; set; }
        [Required]
        public int? steps { get; set; }
        [Required]
        public double? plancalories { get; set; }
        [Required]
        public double? plandistance { get; set; }
        [Required]
        public int? plansteps { get; set; }       
        public Domain.Entity.Main.Day ConvertToBase()
        {
            return new Domain.Entity.Main.Day()
            {
                calories = (double)this.calories,
                distance = (double)this.distance,
                steps = (int)this.steps,
                plancalories = (double)this.plancalories,
                plandistance = (double)this.plandistance,
                plansteps = (int)this.plansteps,
            };
            
        }
    }
}
