using e_commerce.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Service.RoleServices
{
    public interface IRoleServices
    {
        Task<List<UserRoleResponseModel>> GetAll();
        Task<UserRoleResponseModel> GetById(int? id);
        Task<UserRoleResponseModel> Create(UserRoleRequestModel userRoleRequest);
        Task<UserRoleResponseModel> Update(UserRoleRequestModel userRoleRequest, int? id);
        Task<bool> Delete(int? id);
    }
}
