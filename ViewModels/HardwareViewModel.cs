using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            AddSensorsRecursively(_hardware);
        }

        //methods
        private void AddSensorsRecursively(IHardware hardware)
        {
            foreach (ISensor sensor in hardware.Sensors)
            {
                HardwareSensors.Add(new SensorViewModel(sensor));
            }

            foreach (IHardware subHardware in hardware.SubHardware)
            {
                AddSensorsRecursively(subHardware);
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
