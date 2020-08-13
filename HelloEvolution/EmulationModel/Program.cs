using System;
using System.Collections.Concurrent;
using EmulationModel.Interfaces;
using EmulationModel.Commands;
using Ninject;
using Ninject.Extensions.Conventions;

namespace EmulationModel
{
	class Program
	{
		static void Main(string[] args)
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
			container.Bind<StatusMonitor>().ToSelf();
			container.Bind<Emulation>().ToSelf();

			var emulation = container.Get<Emulation>();
			emulation.Start();
			foreach (var count in emulation.StatusMonitor.GenIterationsStatistics)
				Console.WriteLine(count);
			Console.WriteLine($"|{emulation.StatusMonitor.GenerationNumber}|");
		}
	}
}