using System;
using System.Collections.Generic;
using System.Text;

namespace Application.POCOs
{
    public class IdentityResult
    {
        public List<string> Errors = new List<string>();
        public bool Succeeded { get; set; }
        public override string ToString()
        {
            return Succeeded ?
                   "Succeeded" :
                   string.Format("{0} : {1}", "Failed", string.Join("/n", Errors));
        }
    }   
}
