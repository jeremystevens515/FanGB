using System;
using System.ComponentModel;
using LibreHardwareMonitor.Hardware;
using RAMSPDToolkit.Logging;

namespace FanGB.ViewModels
{
    public class SensorViewModel : INotifyPropertyChanged
    {
        //fields
        private readonly ISensor _sensor;
        private float? _value;
        
        
        //INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //properties
        public string SensorName { get => _sensor.Name; }
        public Identifier SensorID { get => _sensor.Identifier; }
        public string SensorType { get => _sensor.SensorType.ToString(); }
        public float? SensorValue         {
            get { return _value; }
            private set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged(nameof(SensorValue));
                }
            }
        }

        //constructor
        public SensorViewModel(ISensor sensor)
        {
            _sensor = sensor;
            _value = sensor.Value;
        }

        //methods
        public void Refresh()
        {
            SensorValue = _sensor.Value;
        }
    }
}
