using Domain.Entity.API;
using Domain.Entity.Main;
using StepMaster.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Db.Interfaces_Repository
{
    public interface IRating_Repository
    {
        Task<BaseResponse<Rating>> GetRating(string regionId);

        Task<BaseResponse<Rating>> CreateRating(string regionId);

        Task<BaseResponse<Rating>> UpdateRating(Rating newRating);

        Task<BaseResponse<Rating>> SetRating(Rating newRating);


    }
}
