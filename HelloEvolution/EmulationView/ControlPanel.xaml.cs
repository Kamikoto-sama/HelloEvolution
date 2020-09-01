using System;
using System.Windows;

namespace EmulationView
{
    public partial class ControlPanel : Window
    {
        public ControlPanel()
        {
            InitializeComponent();
        }

        protected override void OnActivated(EventArgs e)
        {
            Width = Owner.Width;
            Height = Owner.Height;
            base.OnActivated(e);
        }
    }
}