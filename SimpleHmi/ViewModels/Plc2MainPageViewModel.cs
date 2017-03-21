using Prism.Commands;
using Prism.Mvvm;
using SimpleHmi.Plc2Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleHmi.ViewModels
{
    class Plc2MainPageViewModel : BindableBase
    {
        private readonly IPlc2Service _plc2Service;

        public bool FirstOutput
        {
            get { return _firstOutput; }
            set { SetProperty(ref _firstOutput, value); }
        }
        private bool _firstOutput;

        public ICommand WriteFirstOutputCommand { get; private set; }

        public Plc2MainPageViewModel(IPlc2Service plc2Service)
        {
            _plc2Service = plc2Service;
            _plc2Service.ValuesRefreshed += OnPlc2Service_ValuesRefreshed;

            WriteFirstOutputCommand = new DelegateCommand(WriteFirstOutput);
        }

        private void OnPlc2Service_ValuesRefreshed(object sender, EventArgs e)
        {
            FirstOutput = _plc2Service.FirstOutput;
        }

        private void WriteFirstOutput()
        {
            _plc2Service.WriteFirstOutput(!FirstOutput);
        }
    }
}
