using Infrastructure.Enums;
using Application.Services;
using Core.Context;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.POCOs;
using Core.EnumsAndConsts;

namespace Application.CQRS.Common.Queries
{
    public class IsEmailConfirmedQuery : IRequest<IsEmailConfirmedViewModel>
    {
        public string Email { get; set; }
    }
    public class IsEmailConfirmedViewModel
    {
        public RequestResponse Response = new RequestResponse();
        public bool IsConfirmed { get; set; }
    }
    public class IsEmailConfirmedQueryHandler : IRequestHandler<IsEmailConfirmedQuery, IsEmailConfirmedViewModel>
    {
        private ApplicationContext _context;
        private IUserManager _userManager;
        public IsEmailConfirmedQueryHandler(ApplicationContext context, IUserManager userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        public async Task<IsEmailConfirmedViewModel> Handle(IsEmailConfirmedQuery request, CancellationToken cancellationToken)
        {
            IsEmailConfirmedViewModel model = new IsEmailConfirmedViewModel();
            try
            {

                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user != null)
                {
                    model.IsConfirmed = user.EmailConfirmed;
                    model.Response.Result = true;
                    model.Response.Status = (int)EnumResponseStatus.Success;

                    if (user.EmailConfirmed)
                    {
                        model.Response.Msg = "Email is Confirmed";
                    }
                    else
                    {
                        model.Response.Msg = "Email Not Confirmed";
                    }
                }
                else
                {
                    model.Response.Result = false;
                    model.Response.Status = (int)EnumResponseStatus.InvalidData;
                    model.Response.Msg = "No User Exists with Email Provided";
                    model.IsConfirmed = false;
                }
            }
            catch (Exception ex)
            {
                model.Response.Result = false;
                model.Response.Status = (int)EnumResponseStatus.Exception;
                model.Response.Msg = "Exception While Quering with provided Email";
                model.IsConfirmed = false;
            }
            return model;
        }
    }
}
