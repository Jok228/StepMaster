namespace Application.Repositories.Db.Interfaces_Repository
{
    public interface IBase_Repository<T>
    {

        public Task<T> GetObjectBy(string idOrEmail);

        public Task<T> SetObject(T value);

        public Task<T> UpdateObject(T newValue);

        public Task<T> DeleteObject(string idOrEmail);
    }
}
