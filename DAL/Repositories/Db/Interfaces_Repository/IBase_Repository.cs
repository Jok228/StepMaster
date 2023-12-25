using Domain.Entity.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Db.Interfaces_Repository
{
    public interface IBase_Repository<T>
    {

        public Task<BaseResponse<T>> GetObjectBy(string idOrEmail);

        public Task<BaseResponse<T>> SetObject(T value);

        public Task<BaseResponse<T>> UpdateObject(T newValue);
    }
}
