using Application.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Application.Services
{
    public class AuthenticationRulesValidator
    {

        private int minPasswordLength = 6;

        public IdentityResult ValidateEmailFormat(string email)
        {
            IdentityResult identityResult = new IdentityResult();

            bool result = CheckValidEmail(email);

            if (result)
            {
                identityResult.Succeeded = true;
            }
            else
            {
                identityResult.Succeeded = false;
                identityResult.Errors.Add("Invalid Email Address");
            }

            return identityResult;
        }
        private bool CheckValidEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }


        public IdentityResult ValidatePasswordFormat(string password)
        {
            IdentityResult result = new IdentityResult();


            var v1 = CheckPasswordLength(password);
            var v2 = ContainsUpperCaseLetter(password);
            var v3 = ContainsLowerCaseLetter(password);
            var v4 = ContainsSpecialCharacter(password);
            var v5 = ContainsDigit(password);

            result.Succeeded = CheckIfAnyFalse(v1, v2, v3, v4, v5);
            if (!v1) result.Errors.Add("Password Should be " + minPasswordLength + " long");
            if (!v2) result.Errors.Add("Password Should have atleast One Upper Case Character");
            if (!v3) result.Errors.Add("Password Should have atleast One Lower Case Character");
            if (!v4) result.Errors.Add("Password Should have atleast One Special Case Character");
            if (!v5) result.Errors.Add("Password Should have atleast One digit");

            return result;
        }

        private bool CheckPasswordLength(string password)
        {
            return password.Length > minPasswordLength ? true : false;
        }
        private bool ContainsUpperCaseLetter(string password)
        {

            return password.Any(char.IsUpper);
        }
        private bool ContainsSpecialCharacter(string password)
        {
            return password.Any(ch => !Char.IsLetterOrDigit(ch));
        }
        private bool ContainsLowerCaseLetter(string password)
        {
            return password.Any(char.IsLower);
        }
        private bool ContainsDigit(string password)
        {
            return password.Any(ch => Char.IsDigit(ch));
        }

        public IdentityResult MerageIndentityResults(params IdentityResult[] list)
        {
            IdentityResult result = new IdentityResult();

            foreach (var item in list)
            {
                result.Succeeded = item.Succeeded;
                if (!result.Succeeded) break;
            }
            if (!result.Succeeded)
                foreach (var item in list)
                {
                    result.Errors.AddRange(item.Errors);
                }

            return result;
        }
        public bool CheckIfAnyFalse(params bool[] list)
        {
            foreach (var item in list)
            {
                if (!item)
                {

                    return false;
                }
            }
            return true;
        }
    }
}
