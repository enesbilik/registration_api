using dotnet_registration_api.Data.Entities;
using dotnet_registration_api.Data.Models;
using dotnet_registration_api.Data.Repositories;
using dotnet_registration_api.Helpers;
using Mapster;

namespace dotnet_registration_api.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<User>> GetAll()
        {
            throw new NotImplementedException();
        }
        public async Task<User> GetById(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<User> Login(LoginRequest login)
        {
            throw new NotImplementedException();
        }
        public async Task<User> Register(RegisterRequest register)
        {
            if (string.IsNullOrEmpty(register.Password))
            {
                throw new AppException("Password cannot be empty");
            }

            if (await _userRepository.GetUserByUsername(register.Username) != null)
            {
                throw new AppException("Username is already taken");
            }

            string hashedPassword = HashHelper.HashPassword(register.Password);

            var user = new User
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Username = register.Username,
                PasswordHash = hashedPassword
            };

            await _userRepository.CreateUser(user);

            return user;
        }
        public async Task<User> Update(int id, UpdateRequest updateRequest)
        {
            throw new NotImplementedException();
        }
        public async Task Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
