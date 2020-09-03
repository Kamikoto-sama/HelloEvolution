using System.Windows;
using EmulationModel;

namespace EmulationView
{
    public partial class EmulationConfigWindow : Window
    {
        public EmulationConfigWindow(EmulationConfig config)
        {
            InitializeComponent();
            PropertyGrid.SelectedObject = config;
        }
    }
}