using StepMaster.Models.Entity;

namespace StepMaster.Services.Interfaces
{
    public interface IBodies_Service
    {
        Task<Body> GetBodyByEmail(string email);

        Task<Body> SetEditBody(Body body);
    }
}
