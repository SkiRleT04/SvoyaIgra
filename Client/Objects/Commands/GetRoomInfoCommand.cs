﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Client.ViewModels;
using Core.Enums;
using Core.Objects;
using Core.Packets.Response;
using Newtonsoft.Json;
using SvoyaIgraClient;
using SvoyaIgraClient.Views;
using SvoyaIgraClient.Views.GameFrames;

namespace Client.Objects.Commands
{
    class GetRoomInfoCommand : BaseCommand
    {
        public override RequestType RequestType => RequestType.GetRoomInfo;

        public override int Frequency => 4;

        public override void Execute(string packet)
        {
            GetRoomInfoResponse getRoomInfoResponse = JsonConvert.DeserializeObject<GetRoomInfoResponse>(packet);
            //GameViewModel (ClientObject.view as GameViewModel) = ClientObject.view as GameViewModel;
            /*Application.Current.Dispatcher.Invoke(() =>
            {
                SetPage(new RoomsPage());
            });*/

           (ClientObject.view as GameViewModel).Players = new ObservableCollection<Player>(getRoomInfoResponse.Players);

            if(getRoomInfoResponse.TableQuestions != null)
            {
                (ClientObject.view as GameViewModel).QuestionsTable = getRoomInfoResponse.TableQuestions;

                foreach (var item in getRoomInfoResponse.TableQuestions)
                {
                    foreach (var i in item.Value)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            if(i.Id==0)
                                (((((MainWindow)Application.Current.MainWindow).Frame.Content as Game).GameFrame.Content) as CategoriesAndQuestionsTable)?.HideButton(i.Id);
                         });


                        Debug.Write(i.Id != 0 ? 1 : 0);
                        
                    }
                    Debug.WriteLine("");
                }
                
            }



            if (getRoomInfoResponse.Selector != null)
            {
                bool b = ClientObject.user.Login == getRoomInfoResponse.Selector.Login;


                Application.Current.Dispatcher.Invoke(() =>
            {


                (((((MainWindow)Application.Current.MainWindow).Frame.Content as Game).GameFrame.Content) as CategoriesAndQuestionsTable).ChangeButtonEProp(b);
            });
            }
        }
    }
}
