using StepMaster.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ForDb.Interfaces
{
    public interface IRating_Service
    {
        Task<PlaceUserOnRating> GetRatingUser(string email, string region);

    }
}
