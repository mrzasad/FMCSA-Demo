using Application.CQRS.CommandModels;
using Application.Services;
using Core.Context;
using Core.EnumsAndConsts;
using Infrastructure.Email;
using Infrastructure.OTP;
using Infrastructure.SMS;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Common.Commands
{
    public class SendEmailOTPCommand : IRequest<SendEmailViewModel>
    {
        public string Email { get; set; }
    }
    public class SendEmailViewModel
    {
        public CommandResponse CommandResponse = new CommandResponse();
        public string EmailOTP { get; set; }
    }
    public class SendEmailOTPCommandHandler : IRequestHandler<SendEmailOTPCommand, SendEmailViewModel>
    {
        private ApplicationContext _context;
        private IUserManager _userManager;
        public SendEmailOTPCommandHandler(ApplicationContext context, IUserManager userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        public async Task<SendEmailViewModel> Handle(SendEmailOTPCommand request, CancellationToken cancellationToken)
        {
            SendEmailViewModel model = new SendEmailViewModel();
            try
            {
                    OTPProvider oTPProvider = new OTPProvider();
                    model.EmailOTP = oTPProvider.GetOTP();
                    EmailServiceProvider emailServiceProvider = new EmailServiceProvider();
                    emailServiceProvider.SendOTPTOEmail(request.Email, model.EmailOTP);
                    model.CommandResponse.result = true;
                    model.CommandResponse.status = (int)EnumResponseStatus.Success;
                    model.CommandResponse.msg = "";
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