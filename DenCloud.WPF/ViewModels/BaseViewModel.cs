using System.ComponentModel;
using PropertyChanged;

namespace DenCloud.WPF.ViewModels {
    [AddINotifyPropertyChangedInterface]

    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
