using e_commerce.Model.Models;

namespace e_commerce.Service.UserServices
{
    public interface IUserServices
    {
        Task<UserResponseModel> GetUserById(int? id);
        Task<List<UserResponseModel>> GetUserAll();
        Task<UserResponseModel> CreateUser(UserRequestModel userRequest);
        Task<UserResponseModel> DeleteUser(int? id);
        Task<UserResponseModel> UpdateUser(UserRequestModel user, int? id);
    }
}
