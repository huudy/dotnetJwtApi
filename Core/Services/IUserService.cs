using System.Threading.Tasks;
using myJWTAPI.Core.Models;
using myJWTAPI.Core.Services.Communication;

namespace myJWTAPI.Core.Services
{
    public interface IUserService
    {
         Task<CreateUserResponse> CreateUserAsync(User user, params ERole[] userRoles);
         Task<User> FindByEmailAsync(string email);
    }
}