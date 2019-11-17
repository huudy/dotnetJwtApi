using System.Linq;
using System.Threading.Tasks;
using myJWTAPI.Core.Models;
using myJWTAPI.Core.Repositories;
using myJWTAPI.Core.Security.Hashing;
using myJWTAPI.Core.Services;
using myJWTAPI.Core.Services.Communication;
using myJWTAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace myJWTAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly AppDbContext _context;



        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, AppDbContext context)
        {
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _context = context;
        }

        public async Task<CreateUserResponse> CreateUserAsync(User user, params ERole[] userRoles)
        {
            var existingUser = await _context.Users.Where(u => u.Email == user.Email).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                return new CreateUserResponse(false, "Email already in use.", null);
            }

            user.Password = _passwordHasher.HashPassword(user.Password);
            for (int i = 0; i < userRoles.Length; i++)
            {
                var role = new UserRole()
                {
                    RoleId = (int)userRoles[i]
                };
                user.UserRoles.Add(role);
            }



            var newUser = await _context.Users.AddAsync(user);


            // await _context.Users.AddAsync(userRoles);
            await _unitOfWork.CompleteAsync();

            return new CreateUserResponse(true, null, user);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _userRepository.FindByEmailAsync(email);
        }

        // public async Task<User> StoreUser(User user)
        // {
        //     return await _context.Users.AddAsync(user);
        // }
    }
}