using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using EmulationModel;
using EmulationModel.Interfaces;
using EmulationModel.Models;
using EmulationModel.Models.WorldObjects;

namespace EmulationView
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly Emulation emulation;
		private bool initialized;
		private bool paused;
		private const int CellSize = 25;

		private readonly EmulationConfigWindow emulationConfigWindow;
		private readonly EmulationStatusMonitor emulationStatusMonitor;

		public MainWindow()
		{
			InitializeComponent();
			emulation = (Emulation) Application.Current.Resources["emulation"];
			emulation.Config.IterationDelayMilliseconds = TimeSpan.FromMilliseconds(100).TotalMilliseconds;
			emulation.StateChanged += state => Dispatcher.Invoke(() => OnEmulationStateChanged(state));
			emulation.GenIterationPerformed += () => Dispatcher.Invoke(OnGenIterationPerformed);
			emulation.RunWorkerCompleted += (_, args) =>
			{
				if (args.Error != null)
					MessageBox.Show(args.Error.Message);
			};

			emulationConfigWindow = new EmulationConfigWindow(emulation.Config);
			emulationStatusMonitor = new EmulationStatusMonitor(emulation.StatusMonitor);
		}

		private void OnEmulationStateChanged(EmulationStateName state)
        {
	        switch (state)
	        {
		        case EmulationStateName.Initialized when initialized:
		        case EmulationStateName.Initialized:
			        initialized = true;
			        InitMap();
			        AdjustToContent();
			        RenderWorldMap();
			        StartEmulationAction.IsEnabled = true;
			        break;
		        case EmulationStateName.InAction:
			        PauseEmulationAction.IsEnabled = true;
			        break;
	        }
	        Title = $"Main window - {state}";
        }

		private void OnGenIterationPerformed()
		{
			RenderWorldMap();
			emulationStatusMonitor.RenderStats();
		}

		private void InitMap()
        {
	        var columnsCount = emulation.Map.Width;
	        var rowsCount = emulation.Map.Height;

	        for (var i = 0; i < columnsCount; i++)
				MainGrid.ColumnDefinitions.Add(new ColumnDefinition{MinWidth = CellSize});
	        for (var i = 0; i < rowsCount; i++)
		        MainGrid.RowDefinitions.Add(new RowDefinition{MinHeight = CellSize});
        }

		private void AdjustToContent()
        {
	        var screenHeight = SystemParameters.PrimaryScreenHeight;
	        var screenWidth = SystemParameters.PrimaryScreenWidth;
	        var girdSize = MainGrid.RenderSize;
	        if (girdSize.Width > screenWidth || girdSize.Height > screenHeight)
		        WindowState = WindowState.Maximized;
	        else
	        {
		        SizeToContent = SizeToContent.WidthAndHeight;
		        SizeToContent = SizeToContent.Manual;
	        }
        }

		private void RenderWorldMap()
        {
	        MainGrid.Children.Clear();
	        foreach (var (position, cell) in emulation.Map)
	        {
		        var worldObjView = GetWorldObjView(cell);
		        MainGrid.Children.Add(worldObjView);
		        Grid.SetColumn(worldObjView, position.X);
		        Grid.SetRow(worldObjView, position.Y);
	        }
        }

		private UIElement GetWorldObjView(IWorldMapObject cell)
        {
	        UIElement objView = new Border
	        {
		        BorderBrush = Brushes.Gray,
		        BorderThickness = new Thickness(1)
	        };
	        switch (cell.Type)
	        {
		        case WorldObjectType.Poison:
			        ((Border) objView).Background = Brushes.DarkRed;
			        break;
		        case WorldObjectType.Wall:
			        ((Border) objView).Background = Brushes.DarkGray;
			        break;
		        case WorldObjectType.Bot:
			        var bot = (Bot) cell;
			        var button = new Button();
			        button.Background = Brushes.BlueViolet;
			        button.Content = bot.Health;
			        button.Click += (sender, _) =>
			        {
				        if (!paused)
					        return;
				        var btn = (Button) sender;
				        MessageBox.Show($"Bot at {Grid.GetColumn(btn)};{Grid.GetRow(btn)}");
			        };
			        objView = button;
			        break;
		        case WorldObjectType.Food:
			        ((Border) objView).Background = Brushes.Green;
			        break;
		        case WorldObjectType.Empty:
			        ((Border) objView).Background = Brushes.Black;
			        break;
	        }

	        return objView;
        }

		private void AdjustToContent_Clicked(object sender, RoutedEventArgs e) => AdjustToContent();

		private void InitEmulationBtn_Clicked(object sender, RoutedEventArgs e)
		{
			emulation.Init();
			MainGrid.Children.Remove(InitEmulationBtn);
		}

		private void StartEmulation_Clicked(object sender, RoutedEventArgs e)
        {
	        emulation.Start();
	        ((MenuItem) sender).IsEnabled = false;
        }

		private void PauseEmulation_Clicked(object sender, RoutedEventArgs e)
        {
	        var menuItem = (MenuItem) sender;
	        if (paused)
	        {
		        emulation.Continue();
		        menuItem.Header = "Pause emulation";
		        paused = false;
	        }
	        else
	        {
		        emulation.Pause();
		        menuItem.Header = "Continue emulation";
		        paused = true;
	        }
        }

        private void ConfigureEmulation_Clicked(object sender, RoutedEventArgs e)
        {
	        emulationConfigWindow.Owner ??= this;
	        emulationConfigWindow.Show();
        }

        private void EmulationStateMonitorBtn_Clicked(object sender, RoutedEventArgs e)
        {
	        emulationStatusMonitor.Owner ??= this;
	        emulationStatusMonitor.Show();
        }
	}
}