using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using LibreHardwareMonitor.Hardware;

namespace FanGB.ViewModels
{
    public class HardwareViewModel : INotifyPropertyChanged
    {
        //fields
        private readonly IHardware _hardware;

        //properties
        public string HardwareName { get => _hardware.Name; }
        public HardwareType HardwareType { get => _hardware.HardwareType; }        
        public ObservableCollection<SensorViewModel> HardwareSensors { get; } = [];

        //INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;

        //constructor
        public HardwareViewModel(IHardware hardware)
        {
            _hardware = hardware;
            //build a single sensor collection from hardware and subhardware layers
            AddSensors(_hardware);
        }

        //methods
        private void AddSensors(IHardware hardware)
        {
            //get sensors from hardware layer
            foreach (ISensor sensor in hardware.Sensors)
            {
                HardwareSensors.Add(new SensorViewModel(sensor));
                Debug.WriteLine("added sensor from hardware layer");
            }

            //get sensors from subhardware layers
            foreach (IHardware subhardware in hardware.SubHardware)
            {
                foreach (ISensor sensor in subhardware.Sensors)
                {
                    HardwareSensors.Add(new SensorViewModel(sensor));
                    Debug.WriteLine("added sensor from subhardware layer");
                }
            }            
        }
        
        public void Refresh()         
        {
            foreach (SensorViewModel sensor in HardwareSensors)
            {
                sensor.Refresh();
            }
        }

    }
}
