using Application.CQRS.CommandModels;
using Application.Services;
using Core.Context;
using Core.EnumsAndConsts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Common.Commands
{
    public class UpdateEmailAddress : IRequest<CommandResponse>
    {
        public string Email { get; set; }
        public string NewEmail { get; set; }
    }
    public class UpdateEmailAddressHandler : IRequestHandler<UpdateEmailAddress, CommandResponse>
    {
        private ApplicationContext _context;
        private IUserManager _userManager;
        public UpdateEmailAddressHandler(ApplicationContext context, IUserManager userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        public async Task<CommandResponse> Handle(UpdateEmailAddress request, CancellationToken cancellationToken)
        {
            try
            {
                CommandResponse data = new CommandResponse();
                var newmailUser = await _userManager.FindByEmailAsync(request.NewEmail);
                if (newmailUser == null)
                {
                    var user = await _userManager.FindByEmailAsync(request.Email);
                    if (user != null)
                    {
                        user.Email = request.NewEmail;
                        user.NormalizedEmail = _userManager.NormalizeKey(request.NewEmail);
                        user.UserName = request.Email;
                        user.NormalizedUserName = _userManager.NormalizeKey(request.NewEmail);
                        user.EmailConfirmed = true;
                        var result = await _userManager.UpdateAsync(user);

                        if (result.Succeeded)
                        {

                            data.result = true;
                            data.status = (int)EnumResponseStatus.Success;
                            data.msg = "Email Updated Successfully";
                        }
                        else
                        {
                            var list = result.Errors;
                            data.result = false;
                            data.status = (int)EnumResponseStatus.Error;
                            data.msg = result.ToString();
                        }
                    }
                    else
                    {
                        data.result = false;
                        data.status = (int)EnumResponseStatus.InvalidData;
                        data.msg = "No User Exists with Email Provided";
                    }
                }
                else
                {
                    data.result = false;
                    data.status = (int)EnumResponseStatus.Error;
                    data.msg = "Another User Already Registered with new email";
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
