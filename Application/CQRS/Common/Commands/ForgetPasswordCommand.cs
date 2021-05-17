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
    public class ForgetPasswordCommand : IRequest<ForgetPasswordResponse>
    {
        public string Email { get; set; }
    }
    public class ForgetPasswordCommandHanldler : IRequestHandler<ForgetPasswordCommand, ForgetPasswordResponse>
    {
        private ApplicationContext _context;
        private IUserManager _userManager;
        public ForgetPasswordCommandHanldler(ApplicationContext context, IUserManager userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        public async Task<ForgetPasswordResponse> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ForgetPasswordResponse data = new ForgetPasswordResponse();

                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user != null)
                {
                    OTPProvider oTPProvider = new OTPProvider();
                    string otp = oTPProvider.GetOTP();

                    //Send Email / SMS to user

                    EmailServiceProvider eProvider = new EmailServiceProvider();

                    bool sent = await eProvider.SendOTPTOEmail(user.Email, otp);

                    if (sent)
                    {

                        data.result = true;
                        data.status = (int)EnumResponseStatus.Success;
                        data.OPT = otp;
                        data.ExpiresIn = DateTime.UtcNow.AddMinutes(10);
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

                var data = new ForgetPasswordResponse
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
