using Prism.Mvvm;
using SimpleHmi.Plc2Service;
using SimpleHmi.PlcService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHmi.ViewModels
{
    class HmiStatusBarViewModel : BindableBase
    {
        public ConnectionStates Plc1ConnectionState
        {
            get { return _plc1ConnectionState; }
            set { SetProperty(ref _plc1ConnectionState, value); }
        }
        private ConnectionStates _plc1ConnectionState;

        public ConnectionStates Plc2ConnectionState
        {
            get { return _plc2ConnectionState; }
            set { SetProperty(ref _plc2ConnectionState, value); }
        }
        private ConnectionStates _plc2ConnectionState;

        public TimeSpan Plc1ScanTime
        {
            get { return _plc1ScanTime; }
            set { SetProperty(ref _plc1ScanTime, value); }
        }
        private TimeSpan _plc1ScanTime;

        public int Plc2ScanTime
        {
            get { return _plc2ScanTime; }
            set { SetProperty(ref _plc2ScanTime, value); }
        }
        private int _plc2ScanTime;

        private readonly IPlcService _plcService;
        private readonly IPlc2Service _plc2Service;

        public HmiStatusBarViewModel(IPlcService plcService, IPlc2Service plc2Service)
        {
            _plcService = plcService;
            _plc2Service = plc2Service;

            _plcService.ValuesRefreshed += OnPlcServiceValuesRefreshed;
            OnPlcServiceValuesRefreshed(null, EventArgs.Empty);

            plc2Service.ValuesRefreshed += OnPlc2ServiceValuesRefreshed;
            OnPlc2ServiceValuesRefreshed(null, EventArgs.Empty);
        }

        private void OnPlc2ServiceValuesRefreshed(object sender, EventArgs e)
        {
            Plc2ConnectionState = _plc2Service.ConnectionState;
            Plc2ScanTime = (int)_plc2Service.ScanTime.TotalMilliseconds;
        }

        private void OnPlcServiceValuesRefreshed(object sender, EventArgs e)
        {
            Plc1ConnectionState = _plcService.ConnectionState;
            Plc1ScanTime = _plcService.ScanTime;
        }
    }
}
