using CloudStorage.Server.Data;
using CloudStorage.Server.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudStorageFTP.WPF.Loggers
{
    public class InterfaceLogger : ILogger
    {
        public void Log(string message, RecordKind kindOfRecord) => throw new NotImplementedException();
    }
}
