using Core.Objects;
using Core.Packets.Request;
using DevExpress.Mvvm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModels
{
    class RoomViewModel : ViewModelBase
    {
        private Room selectedRoom;

        public RoomViewModel()
        {

            Rooms = new ObservableCollection<Room>();
            
        }

        private ObservableCollection<Room> rooms;
        public ObservableCollection<Room> Rooms
        {
            get { return rooms; }
            set
            {
                rooms = value;
                RaisePropertyChanged(() => Rooms);
            }
        }

        public Room SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                selectedRoom = value;
                RaisePropertyChanged(() => SelectedRoom);
            }
        }

        public int Id
        {
            get { return selectedRoom.Id; }
            set
            {
                if (selectedRoom.Id != value)
                {
                    selectedRoom.Id = value;
                    RaisePropertyChanged(() => Id);
                }
            }
        }

        public int Size
        {
            get { return selectedRoom.Size; }
            set
            {
                if (selectedRoom.Size != value)
                {
                    selectedRoom.Size = value;
                    RaisePropertyChanged(() => Size);
                }
            }
        }

        public int PlayersCount
        {
            get { return selectedRoom.PlayersCount; }
            set
            {
                if (selectedRoom.PlayersCount != value)
                {
                    selectedRoom.PlayersCount = value;
                    RaisePropertyChanged(() => PlayersCount);
                }
            }
        }


        public string Name
        {
            get { return selectedRoom.Name; }
            set
            {
                if (selectedRoom.Name != value)
                {
                    selectedRoom.Name = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }

        public ICommand ClickJoin
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    RoomJoinRequest roomJoinRequest = new RoomJoinRequest();
                    roomJoinRequest.User = ClientObject.user;
                    roomJoinRequest.Room = selectedRoom;
                    if (selectedRoom != null)
                    {
                        string jsonSelectedRoom = JsonConvert.SerializeObject(roomJoinRequest);
                        ClientObject.SendMessage(jsonSelectedRoom);
                    }
                });
            }
        }

    }
}
