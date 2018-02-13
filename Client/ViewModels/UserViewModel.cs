using Client;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Core.Packets.Request;
using Core.Objects;

namespace SvoyaIgraClient.ViewModels
{
    public class UserViewModel:ViewModelBase
    {
        private User user;

        public UserViewModel()
        {
            user = new User("", "");
           
        }

        public string Login
        {
            get { return user.Login; }
            set
            {
                if (user.Login != value)
                {
                    user.Login = value;
                    RaisePropertyChanged(() => Login);
                }
            }
        }

        private string status;
        public string Status
        {
            get
            {
                return status;
            }
            set
            {               
                status = value;
                RaisePropertyChanged(() => Status);             
            }
        }

        public string Password
        {
            get { return user.Password; }
            set
            {
                if (user.Password != value)
                {
                    user.Password = value;
                    RaisePropertyChanged(() => Password);
                }
            }
        }

        public ICommand ClickRegister
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    RegisterUserRequest registerUserRequest = new RegisterUserRequest();
                    registerUserRequest.User = user;
                    
                    string jsonUser = JsonConvert.SerializeObject(registerUserRequest);
                    
                    ClientObject.SendMessage(jsonUser);
                    
                });
            }
        }
        
        public ICommand ClickLogin
        {
            get
            {
                return new DelegateCommand(() => {
                    LoginUserRequest loginUserRequest = new LoginUserRequest();
                    loginUserRequest.User = user;
                    ClientObject.user = user;
                    string jsonUser = JsonConvert.SerializeObject(loginUserRequest);
                    ClientObject.SendMessage(jsonUser);
                });
            }
        }
    }
}
