using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SimpleHmi.Infrastructure;
using SimpleHmi.Plc2Service;
using SimpleHmi.PlcService;
using SimpleHmi.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleHmi.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IPlcService _plcService;
        private readonly IPlc2Service _plc2Service;

        public MainWindowViewModel(IRegionManager regionManager, IPlcService plcService, IPlc2Service plc2Service)
        {
            _regionManager = regionManager;
            _plcService = plcService;
            _plc2Service = plc2Service;

            _regionManager.RegisterViewWithRegion(Regions.ContentRegion, typeof(Plc1MainPage));
            _regionManager.RegisterViewWithRegion(Regions.StatusBarRegion, typeof(HmiStatusBar));
            _regionManager.RegisterViewWithRegion(Regions.LeftMenuRegion, typeof(LeftMenu));

            _plcService.Connect();
            _plc2Service.Connect();
        }
    }
}
