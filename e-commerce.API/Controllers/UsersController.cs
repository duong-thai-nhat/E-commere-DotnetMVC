using e_commerce.Model.Models;
using e_commerce.Service.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserServices userServices;
        public UsersController(IUserServices userServices)
        {
            this.userServices = userServices;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await userServices.GetUserAll();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(int? userId)
        {
            if(userId is null || userId <= 0 )
            {
                return BadRequest("Vui lòng truyền UserId và lớn hơn 0");
            }
            var result = await userServices.GetUserById(userId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNew(UserRequestModel user)
        {
            if (ModelState.IsValid)
            {
                return Ok(await userServices.CreateUser(user));
            }
            else
            {
                return BadRequest("Vui lòng truyền thông tin.");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? userId)
        {
            if (userId is null || userId <= 0)
            {
                return BadRequest("Vui lòng truyền UserId và lớn hơn 0");
            }
            return Ok(await userServices.DeleteUser(userId));
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(UserRequestModel user, int userId)
        {
            if (ModelState.IsValid && userId > 0)
            {
                return Ok(await userServices.UpdateUser(user, userId));
            }
            else
            {
                return BadRequest("Vui lòng truyền thông tin.");
            }
        }
    }
}
