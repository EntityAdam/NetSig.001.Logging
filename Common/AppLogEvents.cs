using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class AppLogEvents
    {
        public static EventId Create = new(1000, "Created");
        public static EventId Read = new(1001, "Read");
        public static EventId Update = new(1002, "Updated");
        public static EventId Delete = new(1003, "Deleted");

        public static EventId KeyNotFound = new(2000, "KeyNotFound");
        public static EventId ConnectionFailed = new(2001, "ConnectionFailed");

    }
}
