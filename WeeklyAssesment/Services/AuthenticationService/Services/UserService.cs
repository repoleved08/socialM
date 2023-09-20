using AuthenticationService.Data;
using AuthenticationService.Model;
using AuthenticationService.Model.Dtos;
using AuthenticationService.Services.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Services
{
    public class UserService : IUserInterface
    {
        private readonly AuthDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtGenerator;

        public UserService(AuthDbContext database, UserManager<ApplicationUser> userManager, IJwtService tokenGenerator, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = database;
            _mapper = mapper;
            _jwtGenerator = tokenGenerator;
        }

        public async Task<bool> AssignUserRole(string email, string Rolename)
        {
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(Rolename).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(Rolename)).GetAwaiter().GetResult();
                }

                await _userManager.AddToRoleAsync(user, Rolename);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());
            var isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (!isValid || user == null)
            {
                return new LoginResponseDto();
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtGenerator.GenerateToken(user, roles);

            return new LoginResponseDto
            {
                User = _mapper.Map<UserDto>(user),
                Token = token
            };
        }

        public async Task<string> RegisterUser(RegisterRequestDto registerRequestDto)
        {
            var user = _mapper.Map<ApplicationUser>(registerRequestDto);

            try
            {
                var result = await _userManager.CreateAsync(user, registerRequestDto.Password);

                if (result.Succeeded)
                {
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
