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
            return await _userRepository.GetAllUsers();
        }
        public async Task<User> GetById(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
            throw new NotFoundException("User not found");
            }
            return user;
        }
        public async Task<User> Login(LoginRequest login)
        {
            if (string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
            {
            throw new AppException("Username and password cannot be empty");
            }

            var user = await _userRepository.GetUserByUsername(login.Username);

            if (user == null)
            {
            throw new NotFoundException("Username or password is wrong");
            }
            var hashedPassword = HashHelper.HashPassword(login.Password);

            if (user.PasswordHash != hashedPassword)
            {
            throw new NotFoundException("Username or password is wrong");
            }           

            return user;
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
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            if (!string.IsNullOrEmpty(updateRequest.OldPassword))
            {
                var hashedOldPassword = HashHelper.HashPassword(updateRequest.OldPassword);
                if (user.PasswordHash != hashedOldPassword)
                {
                    throw new AppException("Old password is wrong");
                }
            }

            if (!string.IsNullOrEmpty(updateRequest.Username))
            {
                var existingUser = await _userRepository.GetUserByUsername(updateRequest.Username);
                if (existingUser != null && existingUser.Id != id)
                {
                    throw new AppException("Username is already taken");
                }
            }

            if (!string.IsNullOrEmpty(updateRequest.NewPassword))
            {
                user.PasswordHash = HashHelper.HashPassword(updateRequest.NewPassword);
            }

            if (!string.IsNullOrEmpty(updateRequest.FirstName))
            {
                user.FirstName = updateRequest.FirstName;
            }

            if (!string.IsNullOrEmpty(updateRequest.LastName))
            {
                user.LastName = updateRequest.LastName;
            }

            await _userRepository.UpdateUser(user);
            return user;
        }
        public async Task Delete(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            await _userRepository.DeleteUser(id);
        }

    }
}
