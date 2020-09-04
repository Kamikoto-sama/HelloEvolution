using EmulationModel;
using Syncfusion.Windows.PropertyGrid;

namespace EmulationView.ViewModels
{
    public class EmulationConfigViewModel
    {
        public object EmulationConfig { get; set; }
        public PropertyExpandModes PropertyExpandMode { get; set; }

        public EmulationConfigViewModel(EmulationConfig config)
        {
            EmulationConfig = config;
            PropertyExpandMode = PropertyExpandModes.FlatMode;
        }
    }
}