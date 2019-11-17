using System.Linq;
using System.Threading.Tasks;
using myJWTAPI.Core.Models;
using myJWTAPI.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace myJWTAPI.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user, ERole[] userRoles)
        {
            var roles = await _context.Roles.Where(r => userRoles.Any(ur => ur.ToString() == r.Name))
                                            .ToListAsync();

            foreach(var role in roles)
            {
                user.UserRoles.Add(new UserRole { RoleId = role.Id });
            }
               
            _context.Users.Add(user);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.Users.Include(u => u.UserRoles)
                                       .ThenInclude(ur => ur.Role)
                                       .SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}