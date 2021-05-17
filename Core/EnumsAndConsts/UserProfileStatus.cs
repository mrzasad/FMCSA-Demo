using System;
using System.Collections.Generic;
using System.Text;

namespace Core.EnumsAndConsts
{
    public class UserProfileStatus
    {
        public static readonly int UnVerfied = 1;
        public static readonly int PendingApproval = 2;
        public static readonly int Approved = 3;
        public static readonly int ReUploadDocument = 4;
        public static readonly int Suspended = 5;

        public static string GetStatusMessage(int status)
        {
            if (status == 1)
            {
                return "Please verify your address";
            }

            else if (status == 2)
            {
                return "Account Approval Pending";
            }
            else if (status == 3)
            {
                return "Account Approved";
            }
            else if (status == 4)
            {
                return "Please repload copy of Documents";
            }
            else if (status == 5)
            {
                return "Account Suspended";
            }
            else
            {
                return "Account Unverified yet please check after 24 hours";
            }
        }
    }
}
