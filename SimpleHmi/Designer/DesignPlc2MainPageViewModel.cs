using SimpleHmi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHmi.Designer
{
    class DesignPlc2MainPageViewModel : Plc2MainPageViewModel
    {
        public DesignPlc2MainPageViewModel() : base(new DesignPlc2Service())
        {
            FirstOutput = true;
        }
    }
}
