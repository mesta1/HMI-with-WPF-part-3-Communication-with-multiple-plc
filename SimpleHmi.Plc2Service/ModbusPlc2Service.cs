using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using SimpleHmi.PlcService;
using System.Net.Sockets;
using Modbus.Device;

namespace SimpleHmi.Plc2Service
{
    public class ModbusPlc2Service : IPlc2Service
    {
        private readonly System.Timers.Timer _timer;
        private TcpClient _client;
        private ModbusIpMaster _master;
        private DateTime _lastScanTime;

        public ConnectionStates ConnectionState { get; private set; }

        public TimeSpan ScanTime { get; private set; }

        public bool FirstOutput { get; private set; }

        public event EventHandler ValuesRefreshed;

        public ModbusPlc2Service()
        {
            _timer = new System.Timers.Timer();
            _timer.Elapsed += OnTimerElapsed;
            _timer.Interval = 100;
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (_master == null || (ConnectionState != ConnectionStates.Online))
            {
                return;
            }
            try
            {
                _timer.Stop();
                ScanTime = DateTime.Now - _lastScanTime;
                RefreshValues();
                OnValuesRefreshed();
            }
            finally
            {
                _timer.Start();
            }
            _lastScanTime = DateTime.Now;
        }

        private void RefreshValues()
        {
            var coils = _master.ReadCoils(1, 0, 1);
            FirstOutput = coils[0];
        }

        public void Connect()
        {
            ConnectionState = ConnectionStates.Connecting;
            _client = new TcpClient("127.0.0.1", 502);
            _master = ModbusIpMaster.CreateIp(_client);
            ConnectionState = ConnectionStates.Online;
            _timer.Start();
        }

        public void Disconnect()
        {
            _timer.Stop();
            _master.Dispose();
            _client.Close();
            ConnectionState = ConnectionStates.Offline;
        }

        private void OnValuesRefreshed()
        {
            ValuesRefreshed?.Invoke(this, new EventArgs());
        }

        public Task WriteFirstOutput(bool value)
        {
            return _master.WriteSingleCoilAsync(1, 0, value);
        }
    }
}
