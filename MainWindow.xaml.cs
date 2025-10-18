using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using LibreHardwareMonitor;
using LibreHardwareMonitor.Hardware;

namespace FanGB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class User : INotifyPropertyChanged
        {
            private string name;
            public string Name
            {
                get { return this.name; }
                set
                {
                    if (this.name != value)
                    {
                        this.name = value;
                        this.NotifyPropertyChanged("Name");
                    }
                }
            }
            public event PropertyChangedEventHandler PropertyChanged;
            public void NotifyPropertyChanged(string PropertyName)
            {
                if(this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }

        private ObservableCollection<User> users = new ObservableCollection<User>();

        public MainWindow()
        {
            InitializeComponent();

            Computer computer = new Computer()
            {
                IsCpuEnabled = false,
                IsGpuEnabled = false,
                IsMemoryEnabled = false,
                IsMotherboardEnabled = true,
                IsControllerEnabled = false,
                IsNetworkEnabled = false,
                IsStorageEnabled = false
            };

            computer.Open();
            computer.Accept(new UpdateVisitor());

            List<string> items = new List<string>();
            foreach (IHardware hardware in computer.Hardware)
            {
                items.Add(hardware.Name);
            }
            HardwareList.ItemsSource = items;

            computer.Close();
        }
    }
}