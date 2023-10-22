using AutoMapper;
using e_commerce.Data;
using e_commerce.Model.Models;
using e_commerce.Models;
using e_commerce.Service.RoleServices;
using e_commerce.Service.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace e_commerce.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserServices _userServices;
        private readonly ECommerceDbContext _context;
        private readonly AppSetting _appSettings;
        private readonly IMapper _mapper;
        
        private readonly IRoleServices _roleServices;


        public UsersController(IUserServices userServices, ECommerceDbContext context,
            IOptionsMonitor<AppSetting> optionsMonitor, IMapper mapper, IRoleServices roleServices)
        {
            _userServices = userServices;
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
            _mapper = mapper;
            _roleServices = roleServices; 
            //CHỖ NÀY KHI NÀO TRÙNG TÊN THÌ MỚI DÙNG THIS.  -> VÌ Ở ĐÂY ĐANG KHÁC NHAU DẤU _ NÊN KO CẦN THIS.
            //vÍ DỤ NHƯ  userServices 2 biến nó trùng tên nên có this. để biết được biến này được khai báo ở ngoài chứ ko phải trong contructor.
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _userServices.GetUserAll();

            if (result == null)
                return NotFound("Không tồn tại người dùng nào!");

            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(int? userId)
        {
            if(userId is null || userId <= 0 )
            {
                return BadRequest("Vui lòng truyền UserId và lớn hơn 0");
            }
            var result = await _userServices.GetUserById(userId);

            if (result == null)
                return NotFound("Không tồn tại người dùng nào!");

            return Ok(result);
        }

        #region Login
        [HttpPost("Login")]
        public IActionResult Validate(LoginModel userLogin)
        {
            var user = _context.Users.SingleOrDefault(p =>
                p.UserName == userLogin.UserName && p.PassWord == userLogin.PassWord
            );

            if(user == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid Username/Passwork"
                });
            }

            //Cấp Token
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Authenticate success",
                Data = GenerateToken((_mapper.Map<UserResponseModel>(user)))
            });
        }
        #endregion Login

        #region Logic Token
        private string GenerateToken(UserResponseModel user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("UserName", user.UserName),
                    new Claim("Phone", user.Phone),
                    new Claim("Id", user.Id.ToString()),

                    //Roles

                    new Claim("TokenId", Guid.NewGuid().ToString())
                }),

                Expires = DateTime.UtcNow.AddMinutes(5),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);

            return jwtTokenHandler.WriteToken(token);
        }
        #endregion Logic Token

        [HttpPost]
        public async Task<IActionResult> Create(UserRequestModel user)
        {
            if (!ModelState.IsValid) 
                return BadRequest("Vui lòng nhập thông tin người dùng.");

            //validate role id
            var roleById = await _roleServices.GetById(user.RoleId);
            if(roleById is null )
                return BadRequest("Role người dùng không hợp lệ.");

            var result = await _userServices.CreateUser(user);
            if (result == null)
                return NotFound("Tạo không thành công!");

            return Ok(result); 
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? userId)
        {
            if (userId is null || userId <= 0)
            {
                return BadRequest("Vui lòng truyền UserId và lớn hơn 0");
            }
            var userExists = await _userServices.GetUserById(userId);
            if (userExists == null)
                return NotFound("Người dùng không tồn tại!");

            var isRemoved = await _userServices.DeleteUser(userId);
            if(!isRemoved)
                return NotFound("Xóa người dùng thất bại");

            return Ok("Xóa người dùng thành công!");
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(UserRequestModel user, int userId)
        {
            if (!ModelState.IsValid || userId <= 0)
                return BadRequest("Vui lòng nhập thông tin.");

            var userExists = await _userServices.GetUserById(userId);
            if (userExists == null)
                return NotFound();

            var result = await _userServices.UpdateUser(user, userId);

            if (result == null) 
                return NotFound("Cập nhật thất bại !");

            return Ok(result);
        }
    }
}
