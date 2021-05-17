using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.POCOs;
using Core.Context;
using Core.Entities;
using Core.EnumsAndConsts;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class UserManager : IUserManager
    {
        private readonly ApplicationContext _context;

        public UserManager(ApplicationContext context)
        {
            this._context = context;
        }

        public UserManager()
        {
            this._context = new ApplicationContext();
        }
        public async Task<IdentityResult> CreateAsync(Users user, string password)
        {
            AuthenticationRulesValidator validator = new AuthenticationRulesValidator();

            var v1 = validator.ValidateEmailFormat(user.Email);
            var v2 = validator.ValidatePasswordFormat(password);

            var response = validator.MerageIndentityResults(v1, v2);

            if (response.Succeeded)
            {
                try
                {
                    var euser = await FindByEmailAsync(user.Email);

                    if (euser == null)
                    {
                        user.NormalizedEmail = NormalizeKey(user.Email);
                        user.NormalizedUserName = NormalizeKey(user.Email);
                        //Add Encryption
                        user.PasswordHash = password;
                        _context.Users.Add(user);
                        if (await _context.SaveChangesAsync() > 0)
                        {
                            response.Succeeded = true;
                            response.Errors.Add("Account Created With Provided Email");
                        }
                    }
                    else
                    {
                        response.Succeeded = false;
                        response.Errors.Add("Account already Exists With Provided Email");
                    }
                }
                catch (Exception ex)
                {
                    response.Succeeded = false;
                    response.Errors.Add("Account already Exists With Provided Email");
                }
            }
            return response;
        }
        public async Task<IdentityResult> UpdateAsync(Users user)
        {
            IdentityResult response = new IdentityResult();
            try
            {

                var euser = await FindByEmailAsync(user.Email);

                if (euser != null)
                {
                    _context.Users.Update(user);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        response.Succeeded = true;
                        response.Errors.Add("Updated");
                    }
                }
                else
                {
                    response.Succeeded = false;
                    response.Errors.Add("Account Not Exists");
                }
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Errors.Add(ex.Message);
            }
            return response;
        }
        public async Task<Users> FindByEmailAsync(string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                email = NormalizeKey(email);
                var user = await _context.Users.Include(x => x.UserRoles).FirstOrDefaultAsync(u => u.NormalizedEmail == email, cancellationToken);

                if (user == null)
                {
                    return null;
                }
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public async Task<Users> FindByEmailAsync(long userId, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);

                if (user == null)
                {
                    return null;
                }
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Users> FindByIdAsync(long userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);

                if (user == null)
                {
                    return null;
                }
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<SignInResult> CheckPasswordSignInAsync(Users user, string password)
        {
            if (await CheckPasswordAsync(user, password))
            {
                return SignInResult.Success;
            }

            return SignInResult.Failed;
        }

        public async Task<bool> CheckPasswordAsync(Users user, string password)
        {
            return user.PasswordHash.Equals(password);
        }

        public string NormalizeKey(string key)
        {
            return (key == null) ? null : key.Normalize().ToUpperInvariant();
        }

    }
}
