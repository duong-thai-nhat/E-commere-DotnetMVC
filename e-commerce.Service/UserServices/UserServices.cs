using AutoMapper;
using e_commerce.Data;
using e_commerce.Model.Entities;
using e_commerce.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Service.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly ECommerceDbContext eCommerce;
        private readonly IMapper _mapper;
        public UserServices(ECommerceDbContext eCommerce, IMapper mapper)
        {
            this.eCommerce = eCommerce;
            _mapper = mapper;
        }

        public async Task<List<UserResponseModel>> GetUserAll()
        {
            var users = await eCommerce.Users.Select(user => _mapper.Map<UserResponseModel>(user)).ToListAsync();

            return users;
        }

        public async Task<UserResponseModel> GetUserById(int? userId)
        {
            var user =  await eCommerce.Users.FindAsync(userId);

            return _mapper.Map<UserResponseModel>(user);
        }

        public async Task<UserResponseModel> CreateUser(UserRequestModel userRequest)
        {
            var userEntities = _mapper.Map<UserEntities>(userRequest);

            eCommerce.Users.Add(userEntities);
            await eCommerce.SaveChangesAsync();

            return _mapper.Map<UserResponseModel>(userEntities); ;
        }

        public async Task<UserResponseModel> DeleteUser(int? userId)
        {
            var user = await eCommerce.Users.FindAsync(userId);
            
            if (user == null)
            {
                return null;
            }

            eCommerce.Users.Remove(user);
            await eCommerce.SaveChangesAsync();

            return _mapper.Map<UserResponseModel>(user);
        }

        public async Task<UserResponseModel> UpdateUser(UserRequestModel userRequest, int? userId)
        {
            var user = await eCommerce.Users.FindAsync(userId);

            if (user == null)
            {
                return null;
            }

            _mapper.Map(userRequest, user);
            await eCommerce.SaveChangesAsync();

            return _mapper.Map<UserResponseModel>(user);
        }
    }
}
