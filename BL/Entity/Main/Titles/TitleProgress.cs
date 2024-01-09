using Domain.Entity.Main.Titles;

namespace StepMaster.Models.API.Title
{
    public class TitleProgress
    {
        public int km_dealt { get; set; }
        public int km_needed { get; set;}
        public TitleDb title {  get; set; }

    }
}
