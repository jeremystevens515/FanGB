using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
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
            public KeyValuePair(Identifier identifier, string key, string value)
            {
                ID = identifier;
                Key = key;
                Value = value;
            }

            public Identifier ID { get; }

            public string Key { get; set; }

            public string Value {  get; set; }

            
        }

        private ObservableCollection<KeyValuePair> MonitorData = new ObservableCollection<KeyValuePair>();

        public MainWindow()
        {
            InitializeComponent();

            Computer computer = new Computer()
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
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
                string hardwareName = hardware.Name;
                Grid hardwareGrid = new Grid();
                hardwareGrid.ShowGridLines = true;
                //hardwareGrid.Name = hardwareName;
                HardwareList.ColumnDefinitions.Add(new ColumnDefinition());

                HardwareList.Children.Add(hardwareGrid);

                foreach (IHardware subhardware in hardware.SubHardware)
                {
                    string subhardwareName = subhardware.Name;
                    Grid subhardwareGrid = new Grid();
                    subhardwareGrid.ShowGridLines = true;
                    //subhardwareGrid.Name = subhardwareName;
                    subhardwareGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    subhardwareGrid.ColumnDefinitions.Add(new ColumnDefinition());

                    hardwareGrid.Children.Add(subhardwareGrid);

                    StackPanel sensorNamesPanel = new StackPanel();
                    StackPanel sensorValuesPanel = new StackPanel();

                    subhardwareGrid.Children.Add(sensorNamesPanel);
                    subhardwareGrid.Children.Add(sensorValuesPanel);

                    foreach (ISensor sensor in subhardware.Sensors)
                    {

                        if (sensor.SensorType == SensorType.Fan || sensor.SensorType == SensorType.Control)
                        {
                            string sensorName = sensor.Name;
                            string sensorValue = sensor.Value.ToString();

                            Grid.SetColumn(sensorNamesPanel, 0);
                            Grid.SetColumn(sensorValuesPanel, 1);                            

                            sensorNamesPanel.Children.Add(new TextBlock() { Text = sensorName });
                            sensorValuesPanel.Children.Add(new TextBlock() { Text = sensorValue });

                        }

                    }
                }
            }

        computer.Close();
        }
    }
}