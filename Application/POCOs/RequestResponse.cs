using System;
using System.Collections.Generic;
using System.Text;

namespace Application.POCOs
{
    public class RequestResponse
    {
        public int Status { get; set; }
        public bool Result { get; set; }
        public string Msg { get; set; }
        public RequestResponse()
        {
            this.Msg = "";
        }
    }


}
