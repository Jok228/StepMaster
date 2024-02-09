using Domain.Entity.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Entity.Main.Clan;

namespace Application.Services.Entity.Interfaces_Service
{
    public interface IClan_Service
    {
        public Task<Clan> SetNewClan(Clan newClan);

        public Task<Clan> GetClan(string mongoId);

        public Task<List<ClanLite>> GetAllClansBySortType(string nameSearch, SortType type, string regionName, int numberPage = 0);

        public Task<Clan> AddUser(string mongoIdClan,string email);

        public Task<bool> CheckOwner(string mongoIdClan, string email);

        public Task<Clan> LeaveUser(string mongoIdClan, string email);
       

        public Task UpdateStepsInClanByUser(string email);

        public Task DeleteClan(string mongoIdClan);
    }
}
