using System.ComponentModel;
using System.Windows;
using EmulationModel.Models;

namespace EmulationView
{
    public partial class EmulationStatusMonitor : Window
    {
        private readonly StatusMonitor monitor;

        public EmulationStatusMonitor(StatusMonitor monitor)
        {
            this.monitor = monitor;
            InitializeComponent();
        }

        public void RenderStats()
        {
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}