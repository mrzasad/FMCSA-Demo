using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.CQRS.Authentication.User;
using Application.CQRS.Common.Commands;
using Application.CQRS.Common.Queries;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountController : BaseController
    {
        private readonly IConfiguration _configuration;
        public AccountController(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }

        [HttpPost]
        public async Task<ActionResult> Login([FromBody]UserQuery cmd)
        {
            var data = await Mediator.Send(cmd);
            if (data.ValidLogin)
            {
                var claim = new[] {
                    new Claim(JwtRegisteredClaimNames.Email, cmd.Email)
                };
                var signinKey = new SymmetricSecurityKey(
                  Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));

                int expiryInMinutes = Convert.ToInt32(_configuration["Jwt:ExpiryInMinutes"]);

                var token = new JwtSecurityToken(
                  issuer: _configuration["Jwt:Site"],
                  audience: _configuration["Jwt:Site"],
                  expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                  signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                );

                data.ExpireAt = token.ValidTo;
                data.Token = new JwtSecurityTokenHandler().WriteToken(token);

            }
            return Ok(data);

        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommand cmd)
        {
            return Ok(await Mediator.Send(cmd));
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordCommand cmd)
        {
            return Ok(await Mediator.Send(cmd));
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailCommand cmd)
        {
            return Ok(await Mediator.Send(cmd));
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmPhone([FromBody] ConfirmPhoneCommand cmd)
        {
            return Ok(await Mediator.Send(cmd));
        }

        [HttpPost]
        public async Task<IActionResult> SendEmailOTP([FromBody] SendEmailOTPCommand cmd)
        {
            return Ok(await Mediator.Send(cmd));
        }
        [HttpPost]
        public async Task<IActionResult> SendMobileOTP([FromBody] SendMobileOTPCommand cmd)
        {
            return Ok(await Mediator.Send(cmd));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEmail([FromBody] UpdateEmailAddress cmd)
        {
            return Ok(await Mediator.Send(cmd));
        }


        [HttpGet]
        public async Task<IActionResult> IsMobileConfirmedQuery(string email)
        {
            IsMobileConfirmedQuery isMobileConfirmedQuery = new IsMobileConfirmedQuery();
            isMobileConfirmedQuery.Email = email;
            return Ok(await Mediator.Send(isMobileConfirmedQuery));
        }
        [HttpGet]
        public async Task<IActionResult> IsEmailConfirmedQuery(string email)
        {
            IsEmailConfirmedQuery isEmailConfirmedQuery = new IsEmailConfirmedQuery();
            isEmailConfirmedQuery.Email = email;
            return Ok(await Mediator.Send(isEmailConfirmedQuery));
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand cmd)
        {
            return Ok(await Mediator.Send(cmd));
        }

    }
}