using DenCloud.Core.Connections;
using DenCloud.Core.Data;
using DenCloud.Core.Logging;
using DenCloud.Core.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DenCloud.WPF.Helpers
{
    public class PortsScanner {
        public PortsScanner(ILogger logger)
        {
            this.logger = logger;
        }

        private ILogger logger { get; set; }

        public void ScanForOpenPorts()
        {
            if (ActionsTracker.UsersInfo.Count() != 0)
            {
                logger.Log($"There are active users on this server. Try again when there are no any.", RecordKind.Error);
                return;
            }

            for (int i = DataConnection.MinPort; i < DataConnection.MaxPort; ++i)
            {
                var listener = new TcpListener(IPAddress.Any, i);
                listener.Start();
            }
            for (int i = DataConnection.MinPort; i < DataConnection.MaxPort; ++i)
            {
                try
                {
                    var client = new TcpClient();
                    client.Connect(DefaultServerValues.ServerExternalIP, i);
                    logger.Log($"Port {i} is opened.", RecordKind.Status);
                    client.Close();
                }
                catch
                {
                    logger.Log($"Port {i} is closed.", RecordKind.Status);
                }
            }

        }
    }
}
