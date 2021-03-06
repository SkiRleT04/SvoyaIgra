﻿using Client.ViewModels;
using Core.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client.Views
{
    /// <summary>
    /// Логика взаимодействия для RoomsPage.xaml
    /// </summary>
    public partial class RoomsPage : Page
    {
        public RoomsPage()
        {
            InitializeComponent();
            ClientObject.view = DataContext as RoomViewModel;
            this.Width = System.Windows.SystemParameters.VirtualScreenWidth;
            this.Height = System.Windows.SystemParameters.VirtualScreenHeight;
        }
    }
}
