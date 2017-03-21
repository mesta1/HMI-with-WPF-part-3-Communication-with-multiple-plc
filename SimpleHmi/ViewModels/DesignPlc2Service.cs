using SimpleHmi.Plc2Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleHmi.PlcService;

namespace SimpleHmi.ViewModels
{
    class DesignPlc2Service : IPlc2Service
    {
        public ConnectionStates ConnectionState
        {
            get
            {
                return ConnectionStates.Online;
            }
        }

        public bool FirstOutput
        {
            get
            {
                return true;
            }
        }

        public TimeSpan ScanTime
        {
            get
            {
                return TimeSpan.FromMilliseconds(200);
            }
        }

        public event EventHandler ValuesRefreshed { add { } remove { } } // avoid warning CS0067

        public void Connect()
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public Task WriteFirstOutput(bool value)
        {
            throw new NotImplementedException();
        }
    }
}
