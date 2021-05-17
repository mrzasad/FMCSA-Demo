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
    public class SendMobileOTPCommand : IRequest<SendMobileViewModel>
    {
        public string Email { get; set; }
        public string MobileNumber { get; set; }
    }
    public class SendMobileViewModel
    {
        public CommandResponse CommandResponse = new CommandResponse();
        public string MobileOTP { get; set; }
    }
    public class ResendMobileOTPCommandHandler : IRequestHandler<SendMobileOTPCommand, SendMobileViewModel>
    {
        private ApplicationContext _context;
        private IUserManager _userManager;

        public ResendMobileOTPCommandHandler(ApplicationContext context, IUserManager userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        public async Task<SendMobileViewModel> Handle(SendMobileOTPCommand request, CancellationToken cancellationToken)
        {
            SendMobileViewModel model = new SendMobileViewModel();
            try
            {

                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user != null)
                {

                    //OTP Provider
                    user.PhoneNumber = request.MobileNumber;
                    user.PhoneNumberConfirmed = false;
                    await _userManager.UpdateAsync(user);
                    
                    OTPProvider oTPProvider = new OTPProvider();
                    model.MobileOTP = oTPProvider.GetOTP();

                    EmailServiceProvider emailServiceProvider = new EmailServiceProvider();
                    emailServiceProvider.SendOTPTOEmail(request.Email, model.MobileOTP);
                    

                    //SMSServiceProvider sMSServiceProvider = new SMSServiceProvider();

                    //sMSServiceProvider.SendOTPTOMobile(request.MobileNumber, model.MobileOTP);
                    model.CommandResponse.result = true;
                    model.CommandResponse.status = (int)EnumResponseStatus.Success;
                    model.CommandResponse.msg = "";
                }
                else
                {

                    model.CommandResponse.msg = "No User Exists with Email Provided | Create Account First";
                }

            }
            catch (Exception ex)
            {


                model.CommandResponse.result = false;
                model.CommandResponse.status = (int)EnumResponseStatus.Exception;
                model.CommandResponse.msg = ex.Message + "\n " + ex.InnerException;

            }
            return model;
        }
    }
}