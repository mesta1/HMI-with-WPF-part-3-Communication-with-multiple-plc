using SimpleHmi.PlcService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHmi.Plc2Service
{
    public interface IPlc2Service
    {
        ConnectionStates ConnectionState { get; }

        TimeSpan ScanTime { get; }

        bool FirstOutput { get; }

        event EventHandler ValuesRefreshed;

        void Connect();
        void Disconnect();

        Task WriteFirstOutput(bool value);
    }
}
