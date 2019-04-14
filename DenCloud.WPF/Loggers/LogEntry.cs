using DenCloud.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenCloud.WPF.Loggers
{
    public class LogEntry
    {
        public string LogMessage { get; set; }

        public RecordKind RecordKind { get; set; }
    }
}
