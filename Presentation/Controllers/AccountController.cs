using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.CQRS.Authentication.User;
using Application.CQRS.CommandModels;
using Application.CQRS.Common.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Presentation.Models.Common.Security;

namespace Presentation.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Index()
        {
            return View("Login");

        } 
        [HttpPost]
        public async Task<ActionResult> Login([FromForm]LoginModel cred)
        { 
            if (ModelState.IsValid)
            {
                CommandResponse response = new CommandResponse();
                UserQueryWithType user = new UserQueryWithType();
                user.Email = cred.Email;
                user.Password = cred.Password;
                var result = await Mediator.Send(user);

                if (result.ValidLogin)
                {
                    var session = new SessionModel() { Email = user.Email, Name = result.Name };
                    HttpContext.Session.SetObjectAsJson(SessionContext._Session, session);

                    return RedirectToAction("index", "Home");
                }
                if (result.RequiresTwoFactor)
                { 
                    return RedirectToAction("LoginWith2fa", "Account");
                }
                if (result.IsLockedOut)
                { 
                    return RedirectToAction("Lockout", "Account");
                }
                else
                {
                    ModelState.AddModelError("", response.msg = result.Msg);
                    return View("Login");
                }
            }
            return View("Login");
        }

        public ActionResult ExternalLogin()
        {
            return View("ExternalLogin");
        }
        public ActionResult LoginWith2fa()
        {
            return View("LoginWith2fa");
        }
        public ActionResult LoginWithRecoveryCode()
        {
            return View("LoginWithRecoveryCode");
        }
        public ActionResult Register()
        {
            return View("Register");
        }
        public ActionResult Logout()
        { 
            SessionContext.Destroy();
            return RedirectToAction("Index", "Account", null);
        }
        public ActionResult ResetPassword()
        {
            return View("ResetPassword");
        } 
        [HttpPost]
        public async Task<IActionResult> UpdatePassword([FromForm]ResetPasswordModel cred)
        {
            if (ModelState.IsValid)
            {
                CommandResponse response = new CommandResponse();
                UpdatePasswordByAdminRoleCommand user = new UpdatePasswordByAdminRoleCommand();
                user.Email = cred.Email;
                user.Password = cred.Password;
                var result = await Mediator.Send(user);

                if (result.ValidLogin)
                {
                    ModelState.AddModelError("", response.msg = result.Msg);
                    return RedirectToAction("index", "Account");
                }
                else
                {
                    ModelState.AddModelError("", response.msg = result.Msg);
                    return View("ResetPassword");
                }
            }
            return View("ResetPassword");

        }

        public ActionResult ResetPasswordConfirmation()
        {
            return View("ResetPasswordConfirmation");
        }
        public ActionResult AccessDenied()
        {
            return View("AccessDenied");
        }
        public ActionResult ConfirmEmail()
        {
            return View("ConfirmEmail");
        }
        public ActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }
        public ActionResult ForgotPasswordConfirmation()
        {
            return View("ForgotPasswordConfirmation");
        }
        public ActionResult Lockout()
        {
            return View("Lockout");
        }
         public ActionResult Error()
        {
            return View("Error");
        }

    }
}