using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CQRS.CommandModels
{
    public class ForgetPasswordResponse :CommandResponse
    {
        
        public string OPT { get; set; }
        
        public DateTime ExpiresIn { get; set; }
    }
}
