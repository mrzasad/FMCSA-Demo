using Application.CQRS.CommandModels;
using Infrastructure.Enums;
using Application.Services;
using Core;
using Core.Context;
using Infrastructure.Email;
using Infrastructure.OTP;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.EnumsAndConsts;

namespace Application.CQRS.Common.Commands
{
    public class ConfirmEmailCommand : IRequest<CommandResponse>
    {
        public string Email { get; set; }

    }
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, CommandResponse>
    {
        private ApplicationContext _context;
        private IUserManager _userManager;
        public ConfirmEmailCommandHandler(ApplicationContext context, IUserManager userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        public async Task<CommandResponse> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                CommandResponse data = new CommandResponse();

                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user != null)
                {
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);
                    
                    data.result = true;
                    data.status = (int)EnumResponseStatus.Success;
                    data.msg = "Email Confirmed Successfully";
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