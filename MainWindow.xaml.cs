using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using LibreHardwareMonitor;
using LibreHardwareMonitor.Hardware;

namespace FanGB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class KeyValuePair
        { 
            public KeyValuePair(string key, string value)
            {
                Key = key;
                Value = value;
            }

            public string Key { get; set; }

            public string Value {  get; set; }

            
        }

        private ObservableCollection<KeyValuePair> MonitorData = new ObservableCollection<KeyValuePair>();
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

            foreach (IHardware hardware in computer.Hardware)
            {
                foreach (IHardware subhardware in hardware.SubHardware)
                {
                    foreach (ISensor sensor in subhardware.Sensors)
                    {
                        //MonitorData.Add(new KeyValuePair(sensor.Name, sensor.Value.ToString()));
                        string ItemContent = sensor.Name + " : " + sensor.Value.ToString();
                        ListViewItem item = new ListViewItem();
                        item.Content = ItemContent;
                        HardwareList.Items.Add(item);
                    }
                }
            }
            
            computer.Close();
        }
    }
}