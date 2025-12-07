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
        public string Name { get => _hardware.Name; }
        public HardwareType HardwareType { get => _hardware.HardwareType; }
        public ObservableCollection<SensorViewModel> Sensors { get; } = [];

        //INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;

        //constructor
        public HardwareViewModel(IHardware hardware)
        {
            _hardware = hardware;
            //Add sensors from this hardware to ObservableCollection
            foreach (ISensor sensor in _hardware.Sensors)
            {
                Sensors.Add(new SensorViewModel(sensor));
            }

            //Add sensors from subhardware to ObservableCollection
            foreach (IHardware subHardware in _hardware.SubHardware)
            {
                foreach (ISensor sensor in subHardware.Sensors)
                {
                    Sensors.Add(new SensorViewModel(sensor));
                }
            }
        }

        //methods
        public void Refresh()         
        {
            foreach (SensorViewModel sensor in Sensors)
            {
                sensor.Refresh();
            }
        }

    }
}
