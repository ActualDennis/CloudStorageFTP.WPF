using CloudStorage.Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudStorageFTP.WPF.Loggers
{
    public class LogEntry
    {
        public string Message { get; set; }

        public RecordKind RecordKind { get; set; }
    }
}
