using System.Threading.Tasks;

namespace myJWTAPI.Core.Repositories
{
    public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}