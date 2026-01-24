using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;
using LibreHardwareMonitor.Hardware;
using FanGB.Models;

namespace FanGB.ViewModels
{
    public sealed class MainViewModel : INotifyPropertyChanged, IDisposable
    {
        //fields
        private readonly DispatcherTimer _updateTimer;

        //properties
        public Computer Computer { get; }
        public ObservableCollection<HardwareViewModel> Hardware { get; } = [];

        
        //methods
        private void OnUpdateTimerTick(object? sender, EventArgs e)
        {
            Computer.Accept(new UpdateVisitor());

            foreach (var hardwareViewModel in Hardware)
            {
                hardwareViewModel.Refresh();
            }
        }
        public void Dispose()
        {
            _updateTimer.Stop();
            Computer.Close();
        }

        //INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //constructor
        public MainViewModel(ComputerOptions options)
        {
            Computer = new Computer()
            {
                IsCpuEnabled = options.CPU,
                IsGpuEnabled = options.GPU,
                IsMemoryEnabled = options.Memory,
                IsMotherboardEnabled = options.Motherboard,
                IsControllerEnabled = options.Controller,
                IsNetworkEnabled = options.Network,
                IsStorageEnabled = options.Storage
            };

            Computer.Open();

            //Build view-model collection from current hardware
            foreach (IHardware hardware in Computer.Hardware)
            {
                Hardware.Add(new HardwareViewModel(hardware));
            }

            _updateTimer = new DispatcherTimer(DispatcherPriority.Background)
            {
                Interval = TimeSpan.FromMilliseconds(1000)
            };
            _updateTimer.Tick += OnUpdateTimerTick;
            _updateTimer.Start();
        }
    }
}
