using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductManagement.API.Configuration;
using ProductManagement.Application;
using ProductManagement.Core.Entities;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductManagement.API.Controllers
{
     
    [Route("/v2/api/[controller]")]
   
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IUserLoginService _userLogin;
        public readonly IConfiguration _configuration;
        //public readonly ConfigurationManager _configurationManager;
        public ProductController(IUnitOfWork unitOfWork,IUserLoginService userLogin,IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _userLogin = userLogin;
            _configuration = configuration;
          
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return Ok(products);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            return Ok(product);

        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            var result = await _unitOfWork.Products.AddAsync(product);
            return Ok(result);
        }

        // PUT api/<ProductController>/5
        [Authorize]
        [HttpDelete]
      
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitOfWork.Products.DeleteAsync(id);
            return Ok(result);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserAuth userLogin)
        {
            //UserAuth userAuth = new UserAuth();
            Validation validation =new Validation();

            if (!validation.Validate(userLogin).IsValid)
            {
                return BadRequest(new { Status = 400, Message = validation.Validate(userLogin).Errors.Select((x) => x.ErrorMessage)});
            }
            else
            {
                var result = await _userLogin.AddUserAsync(userLogin);
                if (result == 0)
                {
                    return BadRequest(new { Status = 400, Message = "User Name Already Exist" });
                }
                else
                {   Log.Debug($"User `{@result} added`");
                    Log.Information($"User `{@result} added `");
                    return Ok(result);
                }
            }
           
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(string UserName,string PassWord)
        {
            var result = await _userLogin.GetCredentialAsync(UserName,PassWord);
            if (result == null)
            {
                //return BadRequest(new { Status = 400, Message = "Invalid User Name or Password" }) ;
                return Unauthorized();
            }
           
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationClass.AppSetting["JWT:Secret"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: ConfigurationClass.AppSetting["JWT:ValidIssuer"],
                audience: ConfigurationClass.AppSetting["JWT:ValidAudience"],
                claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(6),
                    signingCredentials: signinCredentials

                );

            var  tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new { Status = 200, Message = "", Data = tokenString.ToString() });

        }


    }
}
