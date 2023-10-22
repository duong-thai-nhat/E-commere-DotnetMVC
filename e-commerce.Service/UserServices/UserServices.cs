using AutoMapper;
using e_commerce.Data;
using e_commerce.Model.Entities;
using e_commerce.Model.Models;
using e_commerce.Service.RoleServices;
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
            var users = await (from u in _context.Users
                               join r in _context.UserRoles
                               on u.RoleId equals r.Id
                               select new UserResponseModel
                               {
                                   Id = u.Id,
                                   FullName = u.FirstName + u.LastName,
                                   UserName = u.UserName,
                                   Email = u.Email,
                                   Phone = u.Phone,
                                   Role = r.RoleName
                               }).ToListAsync();

            return users;
        }

        public async Task<UserResponseModel> GetUserById(int? userId)
        {
            var userById = await (from u in _context.Users
                                  join r in _context.UserRoles
                                  on u.RoleId equals r.Id
                                  where u.Id == userId
                                  select new UserResponseModel
                                  {
                                      Id = u.Id,
                                      FullName = u.FirstName + u.LastName,
                                      UserName = u.UserName,
                                      Email = u.Email,
                                      Phone = u.Phone,
                                      Role = r.RoleName
                                  }).SingleOrDefaultAsync();

            return userById ?? new UserResponseModel();
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
            //ủa nãy chạy đúng r mà. "role": "e_commerce.Model.Entities.UserRoleEntities" -> nó chưa map ra value 
            // tức là nó ra role: admin mới đúng hả?. uh
            //rồi đó. còn cái update tự làm đi. 
            // ở lúc tạo or update -> phải thêm case không cho trùng user email, call db check rồi trả ra bad request. 
            //ok
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