using EmulationModel.DefaultImplementations;
using EmulationModel.Interfaces;
using Ninject;
using Ninject.Extensions.Conventions;

namespace EmulationModel
{
    public static class EmulationDependenciesConfigurator
    {
        public static Emulation GetConfiguredEmulation()
        {
            var container = new StandardKernel();

            container.Bind(kernel => kernel
                .FromThisAssembly()
                .SelectAllClasses()
                .InheritedFrom<ICommandFactory>()
                .BindAllInterfaces());
            container.Bind<ICommandsCollection>().To<DefaultCommandsCollection>();
            container.Bind<IGenerationBuilder>().To<DefaultGenerationBuilder>();
            container.Bind<IGenomeBuilder>().To<DefaultGenomeBuilder>();
            container.Bind<IWorldMapFiller>().To<DefaultMapFiller>();
            container.Bind<IWorldMapProvider>().To<DefaultMapProvider>();
            container.Bind<EmulationConfig>().ToSelf();
            container.Bind<Emulation>().ToSelf();

            var emulation = container.Get<Emulation>();
            return emulation;
        }

    }
}