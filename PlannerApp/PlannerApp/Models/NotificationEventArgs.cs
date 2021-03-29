using System;
using System.Collections.Generic;
using System.Text;

namespace PlannerApp.Models
{
    public class NotificationEventArgs: EventArgs
    {
        //Title
        public string Title { get; set; }
        //Message
        public string Message { get; set; }
    }
}
