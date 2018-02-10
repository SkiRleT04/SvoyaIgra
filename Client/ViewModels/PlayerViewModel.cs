using SvoyaIgraClient.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SvoyaIgraClient.ViewModels
{
    public class PlayerViewModel : INotifyPropertyChanged
    {
        private Player player;

        public PlayerViewModel()
        {
            player = new Player("123", "321", "333");
        }

        public string PlayerNickName
        {
            get { return player.NickName; }
            set
            {
                if (player.NickName != value)
                {
                    player.NickName = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string PlayerLogin
        {
            get { return player.Login; }
            set
            {
                if (player.Login != value)
                {
                    player.Login = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string PlayerPassword
        {
            get { return player.Password; }
            set
            {
                if (player.Password != value)
                {
                    player.Password = value;
                    RaisePropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            /* 
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            */

            //Тоже самое что выше, но упрощенный вызов (C# 6.0)
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
