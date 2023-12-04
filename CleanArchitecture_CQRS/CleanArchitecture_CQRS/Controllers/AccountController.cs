
using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture_CQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("SignUp/{IsAdmin}")]
        public async Task<IActionResult> SignUpAccount(SignUp signUpModel, bool IsAdmin)
        {
            var result = await _accountRepository.SignUpAsync(signUpModel, IsAdmin);
            if (result.Succeeded)
            {
                return Ok(new { Message = "Sign Up successfully!" });
            }

            return BadRequest(new { Message = "Sign Up failed!" });
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignInAccount(SignIn signInModel)
        {
            var result = await _accountRepository.SignInAsync(signInModel);
            if (string.IsNullOrEmpty(result))
            {
                return BadRequest(new { Message = "Sign In failed!" });
            }

            return Ok(new { Message = "Sign In successfully!" , result});
        }

    }
}
