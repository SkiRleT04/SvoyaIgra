using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Core.Enums;
using Core.Packets.Response;
using SvoyaIgraClient;

namespace Client.Objects.Commands
{
    abstract class BaseCommand
    {
        public abstract RequestType RequestType {get;}
        public abstract int Frequency { get; }
        public abstract void Execute(string packet);

        public bool TypesAreEqual(RequestType reqestType)
        {
            return RequestType.Equals(reqestType);
        }

        public void SetPage(Page page)
        {
            //Application.Current.Dispatcher.Invoke(() =>
            //{
                ((MainWindow)Application.Current.MainWindow).Frame.NavigationService.Navigate(page);
            //});
        }

    }
}
