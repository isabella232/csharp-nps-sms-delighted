using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS_NPS_Delighted.Models
{
    public class SMSCallbackModel
    {
        public Identity From { get; set; }
        public string Message { get; set; }
    }

    public class Identity {
        public string Endpoint { get; set; }
    }
}
