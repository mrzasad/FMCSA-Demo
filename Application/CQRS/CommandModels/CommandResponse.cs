using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CQRS.CommandModels
{
    public class CommandResponse
    {
        public int status { get; set; }
        public bool result { get; set; }
        public string msg { get; set; }
    }
}
