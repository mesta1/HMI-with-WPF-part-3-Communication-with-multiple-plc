using SimpleHmi.PlcService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHmi.Plc2Service
{
    public class DummyPlc2Service : IPlc2Service
    {
        private readonly System.Timers.Timer _timer;
        private DateTime _lastScanTime;
        public bool FirstOutput { get; private set; }

        public event EventHandler ValuesRefreshed;

        public DummyPlc2Service()
        {
            _timer = new System.Timers.Timer();
            _timer.Elapsed += OnTimerElapsed;
            _timer.Interval = 100;
        }

        public TimeSpan ScanTime { get; private set; }

        public ConnectionStates ConnectionState { get; private set; }

        private void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _timer.Stop();

            ScanTime = DateTime.Now - _lastScanTime;
            OnValuesRefreshed();
            _timer.Start();
            _lastScanTime = DateTime.Now;
        }

        public void Connect()
        {
            ConnectionState = ConnectionStates.Online;
            _timer.Start();
        }

        public void Disconnect()
        {
            ConnectionState = ConnectionStates.Offline;
            _timer.Stop();
        }

        private void OnValuesRefreshed()
        {
            ValuesRefreshed?.Invoke(this, new EventArgs());
        }

        public Task WriteFirstOutput(bool value)
        {
            return Task.Run(() => FirstOutput = value);
        }        
    }
}
