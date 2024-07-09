using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOIHangout_WPF.HelperClasses
{
    public class NotificationREPORTDisplayModel
    {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string SpecialEvent { get; set; }
            public bool? IsFinished { get; set; }
            public string Email_UserThatSendedReport { get; set; }
            public string UserName_UserThatSendedReport { get; set; }
            public string Email_UserThatWasReported { get; set; }
            public string UserName_UserThatWasReported { get; set; }

    }
}
