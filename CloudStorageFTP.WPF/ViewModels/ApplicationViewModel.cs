using CloudStorage.Server.Authentication;
using CloudStorage.Server.Di;
using CloudStorage.Server.FileSystem;
using CloudStorageFTP.WPF.Helpers;
using CloudStorageFTP.WPF.Loggers;
using DenInject.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CloudStorageFTP.WPF.ViewModels
{

    public class ApplicationViewModel : BaseViewModel
    {

        public ApplicationViewModel()
        {
           
        }

        public ICommand StartServerCommand => new RelayCommand(() => StartServer(null));

        public ICommand StopServerCommand => new RelayCommand(() => StopServer(null));

        public ICommand RestartServerCommand => new RelayCommand(() => RestartServer(null));

        public ObservableCollection<LogEntry> Logs { get; set; }
      
        private void StartServer(object p)
        {
            
        }

        private void StopServer(object p)
        {
            
        }

        private void RestartServer(object p)
        {

        }

        private void ConstructDi()
        {
            var configBuilder = new DiConfigBuilder();

            configBuilder.UseNeccessaryClasses();

            configBuilder.UseFileSystem(typeof(CloudStorageUnixFileSystemProvider), ObjLifetime.Transient);

            configBuilder.UseAuthentication(typeof(FtpDbAuthenticationProvider), ObjLifetime.Singleton);

            configBuilder.UseLogger(typeof(InterfaceLogger), ObjLifetime.Singleton);

            DiContainer.Provider = new DependencyProvider(configBuilder.config);
        }
    }






}
