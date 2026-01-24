using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanGB.Models
{
    public class ComputerOptions
    {
        //fields
        private bool _isCpuEnabled;
        private bool _isGpuEnabled;
        private bool _isMemoryEnabled;
        private bool _isMotherboardEnabled;
        private bool _isControllerEnabled;
        private bool _isNetworkEnabled;
        private bool _isStorageEnabled;

        //properties
        public bool CPU { get => _isCpuEnabled; set => _isCpuEnabled = value; }
        public bool GPU { get => _isGpuEnabled; set => _isGpuEnabled = value; }
        public bool Memory { get => _isMemoryEnabled; set => _isMemoryEnabled = value; }
        public bool Motherboard { get => _isMotherboardEnabled; set => _isMotherboardEnabled = value; }
        public bool Controller { get => _isControllerEnabled; set => _isControllerEnabled = value; }
        public bool Network { get => _isNetworkEnabled; set => _isNetworkEnabled = value; }
        public bool Storage { get => _isStorageEnabled; set => _isStorageEnabled = value; }

        //default constructor with motherboard enabled
        public ComputerOptions()
        {
            _isCpuEnabled = false;
            _isGpuEnabled = false;
            _isMemoryEnabled = false;
            _isMotherboardEnabled = true;
            _isControllerEnabled = false;
            _isNetworkEnabled = false;
            _isStorageEnabled = false;
        }
    }
}
