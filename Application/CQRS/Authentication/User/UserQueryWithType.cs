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
using Core.Entities;
using System;
using Application.POCOs;

namespace Application.CQRS.Authentication.User
{
    public class UserQueryWithType : IRequest<UserQueryWithTypeDTO>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class UserQueryWithTypeDTO
    {
        public bool ValidLogin { get; set; }
        public string Msg { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }

        public bool RequiresTwoFactor { get; set; }
        public bool IsLockedOut { get; set; }

    }

    public class UserQueryWithTypeHandler : IRequestHandler<UserQueryWithType, UserQueryWithTypeDTO>
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;

        public UserQueryWithTypeHandler(ApplicationContext context, IMapper mapper, IUserManager userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<UserQueryWithTypeDTO> Handle(UserQueryWithType request, CancellationToken cancellationToken)
        {
            UserQueryWithTypeDTO model = new UserQueryWithTypeDTO();
            try
            {

                var user = await _userManager.FindByEmailAsync(request.Email);
                if(user != null)
                {
                    var admin = true;//GetAdminRole(user); 
                    var response = await _userManager.CheckPasswordSignInAsync(user, request.Password);
                    if (admin && response.Succeeded)
                    {
                        model.Name = user.UserName;
                        model.UserId = user.UserId;
                        model.ValidLogin = true;
                        model.RequiresTwoFactor = user.TwoFactorEnabled;
                        model.IsLockedOut = user.LockoutEnabled;
                    }
                    else if (admin && !response.Succeeded)
                    {
                        model.ValidLogin = false;
                        model.Msg = "Password is Incorrect";
                    }
                    else if (response.Succeeded && !admin)
                    {
                        model.ValidLogin = false;
                        model.Msg = "You are not Authorize";
                    }
                    else
                    {
                        model.ValidLogin = false;
                    }
                }
                else
                {
                    model.ValidLogin = false;
                    model.Msg = "User Does not Exit";
                }

            }
            catch (System.Exception ex)
            {
                model.ValidLogin = false;
                model.Msg = "No User Exist With Provided Email";

            }
            return model;
        }

        private bool GetAdminRole(Users user)
        {
            bool admin = false;
            if (user != null)
            {
                foreach (var role in user.UserRoles)
                {
                    if (role.RoleClaimId == 1)
                    {
                        admin = true;
                    }
                }
            }

            return admin;
        }
    }


}
