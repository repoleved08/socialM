using AuthenticationService.Model;
using AuthenticationService.Model.Dtos;
using AuthenticationService.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserInterface _userInterface;
        private readonly ResponseDto _response = new ResponseDto();
        private readonly IMessageBus _messageBus;
        private readonly IConfiguration _configuration;

        public UserController(IUserInterface userInterface, IMessageBus messageBus, IConfiguration configuration)
        {
            _userInterface = userInterface;
            _messageBus = messageBus;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseDto>> AddUSer(RegisterRequestDto registerRequestDto)
        {
            var errorMessage = await _userInterface.RegisterUser(registerRequestDto);
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                return BadRequest(new ResponseDto { IsSuccess = false, Message = errorMessage });
            }

            var queueName = _configuration.GetSection("QueuesandTopics:RegisterUser").Get<string>();
            var message = new UserMessage { Email = registerRequestDto.Email, Name = registerRequestDto.Name };
            await _messageBus.PublishMessage(message, queueName);

            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto>> LoginUser(LoginRequestDto loginRequestDto)
        {
            var response = await _userInterface.Login(loginRequestDto);
            if (response.User == null)
            {
                return BadRequest(new ResponseDto { IsSuccess = false, Message = "Invalid Credential" });
            }

            return Ok(new ResponseDto { Result = response });
        }

        [HttpPost("AssignRole")]
        public async Task<ActionResult<ResponseDto>> AssignRole(RegisterRequestDto registerRequestDto)
        {
            var response = await _userInterface.AssignUserRole(registerRequestDto.Email, registerRequestDto.Role);
            if (!response)
            {
                return BadRequest(new ResponseDto { IsSuccess = false, Message = "Error Occured" });
            }

            return Ok(new ResponseDto { Result = response });
        }
    }
}
