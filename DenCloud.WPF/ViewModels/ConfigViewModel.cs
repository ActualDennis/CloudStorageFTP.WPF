using DenCloud.Core.Helpers;
using DenCloud.WPF.Helpers;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DenCloud.WPF.ViewModels
{
    public class ConfigViewModel : BaseViewModel
    {
        public ConfigViewModel(Action goBackCallback)
        {
            this.goBackCallback = goBackCallback;
            PopupMessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2));
        }

        public ICommand GoBackCommand => new RelayCommand(() => GoBack(null));

        public ICommand SaveConfigCommand => new RelayCommand(() => SaveConfig(null));

        public ICommand UpdateFieldsCommand => new RelayCommand(() => UpdateFields(null));



        private Action goBackCallback { get; set; }

        public string BaseServerPath { get; set; }

        public string FtpPort { get; set; }

        public string LoggerPath { get; set; }

        public string PortMax { get; set; }

        public string PortMin { get; set; }

        public string ExternalIp { get; set; }

        public string CertificatePath { get; set; }

        public string PortsOccupationRetries { get; set; }

        /// <summary>
        /// Defines message queue to show to user(On main screen)
        /// </summary>
        public SnackbarMessageQueue PopupMessageQueue { get; set; }

        private void GoBack(object p)
        {
            goBackCallback.Invoke();
        }

        private void SaveConfig(object p)
        {
            XmlConfigHelper.GenerateConfigFile(
                BaseServerPath ,
                CertificatePath,
                FtpPort ,
                LoggerPath ,
                PortMax,
                PortMin,
                ExternalIp,
                PortsOccupationRetries
                );

            UpdateFields(null);
        }

        private void UpdateFields(object p)
        {
            try
            {
                var config = XmlConfigHelper.ParseSettings();

                BaseServerPath = config.BaseDirectory;
                PortMax = config.MaxPort.ToString();
                PortMin = config.MinPort.ToString();
                ExternalIp = config.ServerExternalIP;
                CertificatePath = config.CertificateLocation;
                LoggerPath = config.LoggingPath;
                FtpPort = config.FtpControlPort.ToString();
                PortsOccupationRetries = config.PassiveConnectionRetryFor.ToString();
            }
            catch
            {
                PopupMessageQueue.Enqueue("Error updating fields: Configuration file is incorrect. Fix it and try again.");
            }
        }
    }
}
