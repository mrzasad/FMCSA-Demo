using Application.CQRS.CommandModels;
using Infrastructure.Enums;
using Application.Services;
using Core.Context;
using Core.EnumsAndConsts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Common.Commands
{
    public class UpdatePasswordCommand : IRequest<UpdatePasswordCVM>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; }
    }
    public class UpdatePasswordCVM
    {
        public CommandResponse Response = new CommandResponse();
        public string QBPassword { get; set; }

    }
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, UpdatePasswordCVM>
    {
        private ApplicationContext _context;
        private IUserManager _userManager;
        public UpdatePasswordCommandHandler(ApplicationContext context, IUserManager userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        public async Task<UpdatePasswordCVM> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            UpdatePasswordCVM data = new UpdatePasswordCVM();
            try
            {

                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user != null)
                {
                    user.PasswordHash = request.Password;
                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        data.Response.result = true;
                        data.Response.status = (int)EnumResponseStatus.Success;
                        data.Response.msg = "Password Updated Successfully";
                    }
                    else
                    {
                        data.Response.result = false;
                        data.Response.status = (int)EnumResponseStatus.Error;
                        data.Response.msg = "Invalid User.";
                    }

                }
                else
                {
                    data.Response.result = false;
                    data.Response.status = (int)EnumResponseStatus.InvalidData;
                    data.Response.msg = "No User Exists with Email Provided";
                }
            }
            catch (Exception ex)
            {

                data.Response.result = false;
                data.Response.status = (int)EnumResponseStatus.Exception;
                data.Response.msg = ex.Message;
            }
            return data;
        }
    }
}
