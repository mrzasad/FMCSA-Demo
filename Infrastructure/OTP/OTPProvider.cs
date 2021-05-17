using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.OTP
{
    public class OTPProvider
    {
        public string GetOTP()
        {
            string num = "0123456789";
            int len = num.Length;
            string opt = string.Empty;
            int otpDigit = 4;
            string finalDigit;
            int getIndex;
            for (int i = 0; i < otpDigit; i++)
            {
                do
                {
                    getIndex = new Random().Next(0, len);
                    finalDigit = num.ToCharArray()[getIndex].ToString();
                } while (opt.IndexOf(finalDigit) != -1);
                opt += finalDigit;
            }
            return opt;
        }
    }
}
