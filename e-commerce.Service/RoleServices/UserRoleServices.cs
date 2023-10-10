using AutoMapper;
using e_commerce.Data;
using e_commerce.Model.Entities;
using e_commerce.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Service.RoleServices
{
    public class UserRoleServices : IRoleServices
    {
        private readonly ECommerceDbContext _context;
        private readonly IMapper _mapper;

        public UserRoleServices(ECommerceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UserRoleResponseModel>> GetAll()
        {
            return await _context.UserRoles.Select(userRole => _mapper.Map<UserRoleResponseModel>(userRole)).ToListAsync();
        }

        public async Task<UserRoleResponseModel> GetById(int? id)
        {
            return _mapper.Map<UserRoleResponseModel>(await _context.UserRoles.FindAsync(id));
        }

        public async Task<UserRoleResponseModel> Create(UserRoleRequestModel userRoleRequest)
        {
            var result = new UserRoleResponseModel();
            var userRoleEntities = _mapper.Map<UserRoleEntities>(userRoleRequest);

            _context.UserRoles.Add(userRoleEntities);
            await _context.SaveChangesAsync();

            if (userRoleEntities.Id > 0)
                result = await GetById(userRoleEntities.Id);
            return result;
        }

        public async Task<bool> Delete(int? id)
        {
            bool isRemoved = false;

            var userRole = await _context.UserRoles.FindAsync(id);
            if (userRole != null)
            {
                _context.UserRoles.Remove(userRole);
                var userIdRemoved = await _context.SaveChangesAsync();
                isRemoved = userIdRemoved > 0;
                return isRemoved;
            }

            return isRemoved;
        }

        public async Task<UserRoleResponseModel> Update(UserRoleRequestModel userRoleRequest, int? id)
        {
            var userRole = await _context.UserRoles.FindAsync(id);

            _mapper.Map(userRoleRequest, userRole);
            var userRoleIdUpdated = await _context.SaveChangesAsync();

            if (userRoleIdUpdated > 0)
                return _mapper.Map<UserRoleResponseModel>(userRole);

            return new UserRoleResponseModel();
        }
    }
}
