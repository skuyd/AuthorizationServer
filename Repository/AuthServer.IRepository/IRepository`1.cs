
using AuthServer.Domain;

namespace AuthServer.IRepository
{
    public interface IRepository<T> where T : IBaseEntity, new()
    {

    }
}
