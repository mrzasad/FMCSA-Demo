using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CQRS.CommandModels
{
    public class ResetPasswordResponse : CommandResponse
    { 
        public string AccountPassword { get; set; }  
    } 
}
