using System;
using System.ComponentModel;
using System.Windows;
using FanGB.Models;
using FanGB.ViewModels;

namespace FanGB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(new ComputerOptions());
        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            if (DataContext is MainViewModel viewModel)
            {
                viewModel.Dispose();
            }
        }
    }
}