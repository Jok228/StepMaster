using Domain.Entity.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Entity.Main.Clan;

namespace Application.Repositories.Db.Interfaces_Repository
{
    public interface IClan_Repository:IBase_Operation<Clan>
    {
        Task<List<ClanLite>> GetClansMinToMax(string nameSearch,int numberPage = 0);

        Task<List<ClanLite>> GetClansMaxToMin(string nameSearch,int numberPage = 0);

        Task<List<ClanLite>> GetClansByFreePlace(string nameSearch,int numberPage = 0);

        Task<List<ClanLite>> GetClansMyRegion(string nameSearch,string regionName,int numberPage = 0);

        Task<Clan> GetClanByUser(string email);

        Task<Clan> GetClansById(string mongoId);

        Task<bool> CheckClansByUser(string email);

        Task<Clan> GetClanOrNullByName(string name, string regionName);

        Task<string> GetNumberOfPlace(string mongoId);

        Task<long> GetCount();

        Task<string> GetNumberOfPlaceRegion(string regionName,string mongoId);

        Task<long> GetCountClansInRegion(string regionName);

        Task<List<string>> UpdateRatingClans();
    }
}
