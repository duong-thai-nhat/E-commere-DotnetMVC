using AutoMapper;
using e_commerce.Data;
using e_commerce.Model.Entities;
using e_commerce.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Service.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly ECommerceDbContext _context;
        private readonly IMapper _mapper;

        public UserServices(ECommerceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UserResponseModel>> GetUserAll()
        {
            return await _context.Users.Select(user => _mapper.Map<UserResponseModel>(user)).ToListAsync();
        }

        public async Task<UserResponseModel> GetUserById(int? userId)
        {
            return _mapper.Map<UserResponseModel>(await _context.Users.FindAsync(userId));
        }

        public async Task<UserResponseModel> CreateUser(UserRequestModel userRequest)
        {
            var result = new UserResponseModel();
            var userEntities = _mapper.Map<UserEntities>(userRequest);

            _context.Users.Add(userEntities);
            await _context.SaveChangesAsync();

            if (userEntities.Id > 0)
                result = await GetUserById(userEntities.Id);
            return result;
        }

        public async Task<bool> DeleteUser(int? userId)
        {
            bool isRemoved = false;

            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                var userIdRemoved = await _context.SaveChangesAsync();
                isRemoved = userIdRemoved > 0;
                return isRemoved;
            }

            return isRemoved;
        }

        public async Task<UserResponseModel> UpdateUser(UserRequestModel userRequest, int? userId)
        {
            var user = await _context.Users.FindAsync(userId);

            _mapper.Map(userRequest, user);
            var userIdUpdated = await _context.SaveChangesAsync();

            if (userIdUpdated > 0)
                return _mapper.Map<UserResponseModel>(user);

            return new UserResponseModel();
        }
    }
}