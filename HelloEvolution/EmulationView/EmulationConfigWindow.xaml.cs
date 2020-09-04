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

        private void CloseBtn_Clicked(object sender, RoutedEventArgs e) => Close();

        private void SaveBtn_Clicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this, "Cant save");
        }
    }
}