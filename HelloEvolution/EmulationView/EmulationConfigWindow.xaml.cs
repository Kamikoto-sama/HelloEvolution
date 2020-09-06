using System.ComponentModel;
using System.Windows;
using EmulationModel;
using EmulationView.ViewModels;

namespace EmulationView
{
    public partial class EmulationConfigWindow : Window
    {
        public EmulationConfigWindow(EmulationConfig config)
        {
            InitializeComponent();
            PropertyGrid.SelectedObject = new EmulationConfigViewModel(config);
        }

        private void CloseBtn_Clicked(object sender, RoutedEventArgs e) => Hide();

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void SaveBtn_Clicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this, "Cant save");
        }
    }
}