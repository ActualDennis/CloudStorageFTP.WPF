using CloudStorage.Server.Data;
using CloudStorage.Server.Di;
using CloudStorage.Server.Helpers;
using CloudStorage.Server.Logging;
using CloudStorage.Server.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CloudStorageFTP.WPF.Helpers
{
    public class UserInfoLogger {
        public UserInfoLogger(ILogger logger)
        {
            this.logger = logger;
        }

        private ILogger logger { get; set; }

        public void PrintUsersInfo()
        {
            var enumerable = ActionsTracker.UsersInfo.AsEnumerable();
            if ((enumerable == null) || (enumerable.Count() == 0))
            {
                logger.Log("No users are currently on the server.", RecordKind.Status);
                return;
            }

            foreach (var user in enumerable)
            {
                logger.Log($"User's endpoint: {((IPEndPoint)user.Key).ToString()}", RecordKind.Status);
                logger.Log(user.Value.IsAuthenticated
                    ? $"Authenticated as : {user.Value.UserName}"
                    : $"Currently not authenticated.", RecordKind.Status);

                string value = user.Value.Security switch
                {
                    ConnectionSecurity.ControlConnectionSecured => "Securing only command channel.",
                    ConnectionSecurity.DataChannelSecured => "Securing only data channel.",
                    ConnectionSecurity.Both => "Securing both data and command channels.",
                    ConnectionSecurity.NonSecure => "Non-secured.",
                    _ => "Non-secured."
                };

                logger.Log($"User's security: {value}", RecordKind.Status);

                if (user.Value.IsAuthenticated)
                {

                    var storageInfo = DiContainer.Provider.Resolve<DatabaseHelper>().GetStorageInformation(user.Value.UserName);

                    logger.Log($"Total storage of user {user.Value.UserName} is {BytesToStringFormatted(storageInfo.BytesTotal)}", RecordKind.Status);
                    logger.Log($"Occupied: {BytesToStringFormatted(storageInfo.BytesOccupied)}", RecordKind.Status);
                    logger.Log($"Free: {BytesToStringFormatted(storageInfo.BytesFree)}", RecordKind.Status);

                }
            }
        }

        private string BytesToStringFormatted(long bytes)
        {
            return bytes switch
            {
                long x when x < 1024 => $"{x} Bytes.",
                long x when (x >= 1024) && (x < 1024 * 1024) => $"{(float)x / 1024} kB.",
                long x when (x >= 1024 * 1024) && (x < 1024 * 1024 * 1024) => $"{(float)x / (1024 * 1024)} MB.",
                long x when (x >= 1024 * 1024 * 1024) => $"{(float)x / (1024 * 1024 * 1024)} GB.",
                _ => "Out of range."
            };
        }
    }
}
