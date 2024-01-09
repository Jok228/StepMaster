using Domain.Entity.Main.Titles;

namespace StepMaster.Models.API.TitlesModel
{
    public class ResponseTitle
    {
        public List<GroupTitle> achievements { get; set; }

        public List<GroupTitle> grades { get; set; }
    }
}
