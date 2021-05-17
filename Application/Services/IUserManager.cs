using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application;
using Application.POCOs;
using System.Threading;

namespace Application.Services
{
    public interface IUserManager
    {
        Task<IdentityResult> CreateAsync(Users user, string password);
        Task<IdentityResult> UpdateAsync(Users user);
        Task<Users> FindByEmailAsync(long UserId, CancellationToken cancellationToken = default(CancellationToken));
        Task<Users> FindByEmailAsync(string email, CancellationToken cancellationToken = default(CancellationToken));
        Task<SignInResult> CheckPasswordSignInAsync(Users user, string password);
        Task<bool> CheckPasswordAsync(Users user, string password);
        Task<Users> FindByIdAsync(long userId);
        string NormalizeKey(string key);

    }
}
