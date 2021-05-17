using AutoMapper;
using Core.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Application.Services;
using Core.Entities;
using System;

namespace Application.CQRS.Common.Commands
{ 
    public class UpdatePasswordByAdminRoleCommand : IRequest<UpdatePasswordByAdminRoleCommandDTO>
    { 
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; }
    }
    public class UpdatePasswordByAdminRoleCommandDTO
    {
        public bool ValidLogin { get; set; }
        public string Msg { get; set; }
    }

    public class UpdatePasswordByAdminRoleCommandHandler : IRequestHandler<UpdatePasswordByAdminRoleCommand, UpdatePasswordByAdminRoleCommandDTO>
    {
        private ApplicationContext _context;
        private IUserManager _userManager;

        public UpdatePasswordByAdminRoleCommandHandler(ApplicationContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<UpdatePasswordByAdminRoleCommandDTO> Handle(UpdatePasswordByAdminRoleCommand request, CancellationToken cancellationToken)
        {
            UpdatePasswordByAdminRoleCommandDTO model = new UpdatePasswordByAdminRoleCommandDTO();
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);                
                if(user !=null)
                {
                    var admin = GetAdminRole(user);
                    if (admin)
                    { 
                        user.PasswordHash = request.Password;
                        var result = await _userManager.UpdateAsync(user);
                        model.ValidLogin = true;
                        model.Msg = "Password change Successfully";
                    }
                    else
                    {
                        model.ValidLogin = false;
                        model.Msg = "You are not Authorize";
                    }
                }
                else
                {
                    model.ValidLogin = false;
                    model.Msg = "No User Exist With Provided Email";
                }
            }
            catch (Exception ex)
            {
                model.ValidLogin = false;
                model.Msg = ex.Message;
            }          
                return model;
        }
        private bool GetAdminRole(Users user)
        {
            bool admin = false;
            foreach (var role in user.UserRoles)
            {
                if (role.RoleClaimId == 1)
                {
                    admin = true;
                }
            }
            return admin;
        }
    }
}
