using Application.CQRS.CommandModels;
using Application.Services;
using Core.Context;
using Core.EnumsAndConsts;
using Infrastructure.Email;
using Infrastructure.OTP;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Common.Commands
{
    public class ConfirmPhoneCommand : IRequest<CommandResponse>
    {
        public string Email { get; set; }
        public string MobileNumber { get; set; }

    }
    public class ConfirmPhoneCommandHandler : IRequestHandler<ConfirmPhoneCommand, CommandResponse>
    {
        private ApplicationContext _context;
        private IUserManager _userManager;

        public ConfirmPhoneCommandHandler(ApplicationContext context, IUserManager userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        public async Task<CommandResponse> Handle(ConfirmPhoneCommand request, CancellationToken cancellationToken)
        {
            try
            {
                CommandResponse data = new CommandResponse();

                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user != null)
                {
                    user.PhoneNumberConfirmed = true;
                    await _userManager.UpdateAsync(user);
                    
                    
                    data.result = true;
                    data.status = (int)EnumResponseStatus.Success;
                    data.msg = "Mobile Number Confirmed Successfully";
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

                var data = new CommandResponse
                {
                    result = false,
                    status = (int)EnumResponseStatus.Exception,
                    msg = ex.Message + "\n " + ex.InnerException
                };
                return data;
            }
        }
    }
}