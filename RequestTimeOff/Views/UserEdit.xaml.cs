﻿using RequestTimeOff.ViewModels;
using System;
using System.Collections.Generic;
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

namespace RequestTimeOff.Views
{
    /// <summary>
    /// Interaction logic for UserEdit.xaml
    /// </summary>
    public partial class UserEdit : UserControl
    {
        public UserEdit(UserEditViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
        public UserEdit()
        {
            InitializeComponent();
        }
    }
}