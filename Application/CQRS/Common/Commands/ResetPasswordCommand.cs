using Application.CQRS.CommandModels;
using Application.Services;
using Core.Context;
using Core.EnumsAndConsts;
using Infrastructure.Email;
using Infrastructure.OTP;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Common.Commands
{
    public class ResetPasswordCommand : IRequest<ResetPasswordResponse>
    {
        public string Email { get; set; }
    }
    public class ResetPasswordCommandHanldler : IRequestHandler<ResetPasswordCommand, ResetPasswordResponse>
    {
        private ApplicationContext _context;
        private IUserManager _userManager;
        public ResetPasswordCommandHanldler(ApplicationContext context, IUserManager userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        public async Task<ResetPasswordResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ResetPasswordResponse data = new ResetPasswordResponse();

                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user != null)
                { 
                    //Send Email / SMS to user

                    EmailServiceProvider eProvider = new EmailServiceProvider();

                    bool sent = await eProvider.SendPasswordTOEmail(user.Email, user.PasswordHash);

                    if (sent)
                    {

                        data.result = true;
                        data.status = (int)EnumResponseStatus.Success;
                        data.AccountPassword = user.PasswordHash; 
                    }
                    else
                    {
                        data.result = false;
                        data.status = (int)EnumResponseStatus.InvalidData;
                        data.msg = "Invalid Email Address";
                    }
                }
                else
                {
                    data.result = false;
                    data.status = (int)EnumResponseStatus.InvalidData;
                    data.msg = "No User Exists with Email Provided";
                }
                return data;
            }
            catch (Exception ex)
            {

                var data = new ResetPasswordResponse
                {
                    result = false,
                    status = (int)EnumResponseStatus.Exception,
                    msg = ex.Message + "\n : " + ex.InnerException
                };
                return data;
            }
        }
    }
}
