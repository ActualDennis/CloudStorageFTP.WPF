using CloudStorage.Server;
using CloudStorage.Server.Authentication;
using CloudStorage.Server.Data;
using CloudStorage.Server.Di;
using CloudStorage.Server.FileSystem;
using CloudStorage.Server.Logging;
using CloudStorageFTP.WPF.Data;
using CloudStorageFTP.WPF.Helpers;
using CloudStorageFTP.WPF.Loggers;
using DenInject.Core;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CloudStorageFTP.WPF.ViewModels
{

    public class ApplicationViewModel : BaseViewModel
    {

        public ApplicationViewModel()
        {
            ConstructDi();
            Logs = new ObservableCollection<LogEntry>();
            Logger = (InterfaceLogger)DiContainer.Provider.Resolve<ILogger>();
            Logger.OnLog += OnLogHappened;
            Config = new ConfigViewModel(() => MoveToInitialApplicationScreen());
            YesNoList = new List<string>() { "Yes", "No" };
        }

        public ICommand StartServerCommand => new RelayCommand(() => StartServer(null));

        public ICommand StopServerCommand => new RelayCommand(() => StopServer(null));

        public ICommand RestartServerCommand => new RelayCommand(() => RestartServer(null));

        public ICommand SeeConfigCommand => new RelayCommand(() => SeeConfig(null));

        public ICommand LogUsersCommand => new RelayCommand(() => LogUsers(null));

        public ConfigViewModel Config { get; set; }

        public ObservableCollection<LogEntry> Logs { get; set; }

        private InterfaceLogger Logger { get; set; }

        public List<string> YesNoList { get; set; }

        public string IsEncryptionEnabledString { get; set; } = "Yes";

        public string IsWaitingForUsersToDisconnectString { get; set; } = "Yes";

        public int CurrentAppScreen { get; set; }

        public bool IsStartAvailable { get; set; } = true;

        public bool IsStopAvailable { get; set; } = false;

        public bool IsSeeConfigAvailable { get; set; } = true;

        public FtpServer ServerInstance { get; set; }
     
        private void StartServer(object p)
        {
            if (!IsStartAvailable)
                return;

            var server = DiContainer.Provider.Resolve<FtpServer>();

            Task.Run(() => server.Start(IsEncryptionEnabledString == "Yes"))
                .ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        IsStopAvailable = false;
                        IsStartAvailable = true;
                    }
                });

            IsStopAvailable = true;
            IsStartAvailable = false;
            ServerInstance = server;
        }

        private async void StopServer(object p)
        {
            if (IsStartAvailable)
                return;

            IsStopAvailable = false;

            await ServerInstance.Stop(IsWaitingForUsersToDisconnectString == "Yes")
                .ContinueWith(t => 
                {
                    if (t.IsCompleted)
                    {
                        IsStartAvailable = true;
                        ServerInstance = null;
                    }
                });
        }

        private void RestartServer(object p)
        {
            if (IsStartAvailable)
                return;

            var server = DiContainer.Provider.Resolve<FtpServer>();
        }
        private void LogUsers(object p)
        {
            var userLogger = new UserInfoLogger(Logger);

            userLogger.PrintUsersInfo();
        }

        private void SeeConfig(object p)
        {
            CurrentAppScreen = (int)AppScreen.Configuration;
        }

        private void MoveToInitialApplicationScreen()
        {
            CurrentAppScreen = (int)AppScreen.Main;
        }

        private void OnLogHappened(object sender, LogEntry e)
        {
            App.Current.Dispatcher.BeginInvoke((Action)delegate ()
            {
                Logs.Add(e);
            });
        }

        private void ConstructDi()
        {
            var configBuilder = new DiConfigBuilder();

            configBuilder.UseNeccessaryClasses();

            configBuilder.UseFileSystem(null, true);

            configBuilder.UseAuthentication(null, true);

            configBuilder.UseLogger(typeof(InterfaceLogger), false);

            DiContainer.Construct(configBuilder);
        }
    }






}
