using DenCloud.Core.Data;
using DenCloud.WPF.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DenCloud.WPF.UI
{
    class LogTemplateSelector : DataTemplateSelector
    {

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement elem = container as FrameworkElement;

            var entry = (LogEntry)item;

            switch (entry.RecordKind)
            {
                case RecordKind.CommandReceived:
                    {
                        return elem.FindResource("LogCommandDataTemplate") as DataTemplate;
                    }
                case RecordKind.Error:
                    {
                        return elem.FindResource("LogErrorDataTemplate") as DataTemplate;
                    }
                case RecordKind.Status:
                    {
                        return elem.FindResource("LogStatusDataTemplate") as DataTemplate;
                    }
            }

            throw new ApplicationException();
        }
    }
}
