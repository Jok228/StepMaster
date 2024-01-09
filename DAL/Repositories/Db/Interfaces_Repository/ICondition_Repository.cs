
using Domain.Entity.Main.Titles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Db.Interfaces_Repository
{
    public interface ICondition_Repository
    {
        Task<List<Condition>> GetConditionsAsync();
    }
}
