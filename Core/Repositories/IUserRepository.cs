using System.Threading.Tasks;
using myJWTAPI.Core.Models;

namespace myJWTAPI.Core.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user, ERole[] userRoles);
        Task<User> FindByEmailAsync(string email);
    }
}