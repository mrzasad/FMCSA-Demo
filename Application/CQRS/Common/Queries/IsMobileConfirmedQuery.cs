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
    public class IsMobileConfirmedQuery : IRequest<IsMobileConfirmedViewModel>
    {
        public string Email { get; set; }
    }
    public class IsMobileConfirmedViewModel
    {
        public RequestResponse Response = new RequestResponse();
        public bool IsConfirmed { get; set; }
    }
    public class IsMobileConfirmedQueryHandler : IRequestHandler<IsMobileConfirmedQuery, IsMobileConfirmedViewModel>
    {
        private ApplicationContext _context;
        private IUserManager _userManager;
        public IsMobileConfirmedQueryHandler(ApplicationContext context, IUserManager userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        public async Task<IsMobileConfirmedViewModel> Handle(IsMobileConfirmedQuery request, CancellationToken cancellationToken)
        {
            IsMobileConfirmedViewModel model = new IsMobileConfirmedViewModel();
            try
            {

                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user != null)
                {
                    model.IsConfirmed = user.PhoneNumberConfirmed;
                    model.Response.Result = true;
                    model.Response.Status = (int)EnumResponseStatus.Success;

                    if (user.PhoneNumberConfirmed)
                    {
                        model.Response.Msg = "Mobile is Confirmed";
                    }
                    else
                    {
                        model.Response.Msg = "Mobile Not Confirmed";
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
