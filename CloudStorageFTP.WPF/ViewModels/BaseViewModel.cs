using System.ComponentModel;
using PropertyChanged;

namespace CloudStorageFTP.WPF.ViewModels {
    [AddINotifyPropertyChangedInterface]

    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
