using AutoMapper;
using Core;
using Core.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using System.Linq;
using Application.Services;
using System;

namespace Application.CQRS.Authentication.User
{

    public class UserQuery : IRequest<UserViewModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
    public class UserViewModel
    {
        public bool ValidLogin { get; set; }
        public long UserId { get; set; }
        public DateTime ExpireAt { get; set; }
        public string Token { get; set; }
    }
    public class UserQueryHandler : IRequestHandler<UserQuery, UserViewModel>
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;

        public UserQueryHandler(ApplicationContext context, IMapper mapper, IUserManager userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<UserViewModel> Handle(UserQuery request, CancellationToken cancellationToken)
        {
            UserViewModel model = new UserViewModel();
            try
            {


                var user = await _userManager.FindByEmailAsync(request.Email);
                var response = await _userManager.CheckPasswordSignInAsync(user, request.Password);
                if (response.Succeeded)
                {
                    model.UserId = user.UserId;
                    model.ValidLogin = true;
                }
                else
                {
                    model.ValidLogin = false;
                }
            }
            catch (System.Exception ex)
            {
                model.ValidLogin = false;

            }
            return model;
        }
    }
}
