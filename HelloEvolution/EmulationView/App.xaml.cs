using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using EmulationModel;

namespace EmulationView
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			var emulation = EmulationDependenciesConfigurator.GetConfiguredEmulation();
			Resources.Add("emulation", emulation);
			ShutdownMode = ShutdownMode.OnMainWindowClose;
		}
	}
}