
using Application.Dto;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> SignUpAsync(SignUp model, bool IsAdmin);
        public Task<string> SignInAsync(SignIn model);
    }
}
