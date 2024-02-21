using StepMaster.Models.Entity;

namespace Application.Repositories.Db.Interfaces_Repository
{
    public interface IUser_Repository : IBase_Repository<User>
    {

        Task<User> GetByCookie(string cookie);

        Task<List<User>> GetObjectsByRegion(string regionId);

        Task<List<User>> GetUserByCountry();

        Task<bool> CheckUser(string email);

        #region Friends System

        Task<List<User>> GetUsers(string searchText, string? regionId, int page);

        Task<List<User>> GetOnlineUsersByList(List<string> users, int page, bool isOnline);

        #endregion
    }
}
