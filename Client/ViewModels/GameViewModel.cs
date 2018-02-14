using Client.Views;
using Core.Packets.Request;
using DevExpress.Mvvm;
using Newtonsoft.Json;
using SvoyaIgraClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client.ViewModels
{
    class GameViewModel:ViewModelBase
    {

        public ICommand ClickLeaveRoom
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    RoomLeaveRequest roomLeaveRequest = new RoomLeaveRequest();
                    string jsonLeaveRoom = JsonConvert.SerializeObject(roomLeaveRequest);
                    ClientObject.SendMessage(jsonLeaveRoom);
                });
            }
        }
    }
}
