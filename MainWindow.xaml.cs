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

            /*
            foreach (IHardware hardware in computer.Hardware)
            {
                HardwareList.Items.Add(hardware.Name);

                foreach (IHardware subhardware in hardware.SubHardware)
                {
                    HardwareList.Items.Add(subhardware.Name);

                    foreach (ISensor sensor in subhardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Fan || sensor.SensorType == SensorType.Control)
                        {
                            HardwareList.Items.Add($"{sensor.Name}({sensor.SensorType}) : {sensor.Value.ToString()}");
                        }

                    }
                }
            }
            */

            //cpu fan control value
            //computer.Hardware[0].SubHardware[0].Sensors[0].Value;
            //cpu fan speed value
            //computer.Hardware[0].SubHardware[0].Sensors[8].Value;


            MonitorData.Add(
                new KeyValuePair
                (
                    computer.Hardware[0].SubHardware[0].Sensors[0].Identifier,
                    computer.Hardware[0].SubHardware[0].Sensors[0].Name,
                    computer.Hardware[0].SubHardware[0].Sensors[0].Value.ToString()
                ));

            foreach (KeyValuePair kvp in MonitorData)
            {
                HardwareList.Items.Add($"{kvp.Key} : {kvp.Value}%");
            }

        computer.Close();
        }
    }
}