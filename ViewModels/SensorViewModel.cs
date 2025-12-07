using System;
using System.ComponentModel;
using LibreHardwareMonitor.Hardware;

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
        public float? Value         {
            get { return _value; }
            private set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged(nameof(Value));
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
            Value = _sensor.Value;
        }
    }
}
