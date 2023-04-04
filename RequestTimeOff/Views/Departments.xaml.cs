﻿using RequestTimeOff.MVVM;
using RequestTimeOff.ViewModels;
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
    /// Interaction logic for Departments.xaml
    /// </summary>
    public partial class Departments : UserControl, IPage
    {
        public Departments(DepartmentsViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }
        public Departments()
        {
            InitializeComponent();
        }
    }
}
