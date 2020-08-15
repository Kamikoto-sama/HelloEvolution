using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmulationView
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
			var mapSize = new Size(10, 10);
			var cellSize = new Size(24, 24);
			for (var i = 0; i < mapSize.Width; i++)
			{
				var value = new ColumnDefinition();
				value.Width = new GridLength(cellSize.Width);
				MapField.ColumnDefinitions.Add(value);
			}
			for (var i = 0; i < mapSize.Height; i++)
			{
				var value = new RowDefinition();
				value.Height = new GridLength(cellSize.Height);
				MapField.RowDefinitions.Add(value);
			}
			var button = new Button();
			button.Width = cellSize.Width;
			button.Height = cellSize.Height;
			button.Content = "00";
			MapField.Children.Add(button);
			var button2 = new Button();
			button2.Width = cellSize.Width;
			button2.Height = cellSize.Height;
			button2.Content = "02";
			MapField.Children.Add(button2);
		}
    }
}