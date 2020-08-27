using System;
using EmulationModel.Interfaces;
using Ninject;
using Ninject.Extensions.Conventions;

namespace EmulationModel
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			var emulation = ConfigureDependencies();
			// emulation.Config.DelayType = DelayTypes.PerEachGenIteration;
			emulation.StateChanged += state =>
			{
				if (state != EmulationStateName.Finished) return;
				var stats = emulation.StatusMonitor.GenIterationsStatistics;
				var statsInfo = string.Join(", ", stats);
				Console.WriteLine($"[{stats.Count}] {statsInfo}");
			};
			// emulation.Init();
			// emulation.Start();
			RunCli(emulation);
		}

		private static Emulation ConfigureDependencies()
		{
			var container = new StandardKernel();

			container.Bind(kernel => kernel
				.FromThisAssembly()
				.SelectAllClasses()
				.InheritedFrom<ICommandFactory>()
				.BindAllInterfaces());
			container.Bind<ICommandsCollection>().To<CommandsCollection>();
			container.Bind<IGenerationBuilder>().To<GenerationBuilder>();
			container.Bind<IGenomeBuilder>().To<GenomeBuilder>();
			container.Bind<IWorldMapFiller>().To<MapFiller>();
			container.Bind<IWorldMapProvider>().To<TxtMapProvider>();
			container.Bind<EmulationConfig>().ToSelf();
			container.Bind<Emulation>().ToSelf();
			var emulation = container.Get<Emulation>();
			return emulation;
		}

		private static void RunCli(Emulation emulation)
		{
			// emulation.GenIterationPerformed += () => Console.WriteLine("GenIterationPerformed");
			emulation.StateChanged += state => Console.WriteLine(state);
			emulation.Init();
			while (true)
			{
				var command = Console.ReadLine();
				var result = false;
				switch (command)
				{
					case "start":
						result = emulation.Start();
						break;
					case "pause":
						result = emulation.Pause();
						break;
					case "continue":
						result = emulation.Continue();
						break;
					case "restart":
						result = emulation.Restart();
						break;
					case "q":
						return;
				}

				Console.WriteLine($"[{result}]");
			}
		}
	}
}