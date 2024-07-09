using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOIHangout_WPF.HelperClasses
{
    public class EventDisplayModel
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string SpecialEvent { get; set; }
        public bool? IsFinished { get; set; }
        public string UserEmail { get; set; } // New property for user email
    }
}
