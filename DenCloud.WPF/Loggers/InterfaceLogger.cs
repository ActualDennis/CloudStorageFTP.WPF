using DenCloud.Core.Data;
using DenCloud.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenCloud.WPF.Loggers
{
    public class InterfaceLogger : ILogger
    {

        public event EventHandler<LogEntry> OnLog;
        public void Log(string message, RecordKind kindOfRecord)
        {
            OnLog?.Invoke(null, new LogEntry() { LogMessage = message, RecordKind = kindOfRecord });
        }
    }
}
