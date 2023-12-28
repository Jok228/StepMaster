using Domain.Entity.API;
using MongoDB.Bson.Serialization.Serializers;
using StepMaster.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Db.Interfaces_Repository
{
    public interface IUser_Repository : IBase_Repository<User>
    {

        Task<BaseResponse<User>> GetByCookie(string cookie);

        Task<BaseResponse<List<User>>> GetObjectsByRegion(string regionId);

        Task<bool> DeleteUser(string email);
    }
}
