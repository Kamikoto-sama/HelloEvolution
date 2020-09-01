using System;
using EmulationModel;
using EmulationModel.Models;

namespace Emulator
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var emulation = EmulationDependenciesConfigurator.GetConfiguredEmulation();
            emulation.Config.DelayType = DelayTypes.NoDelay;
            emulation.StateChanged += state =>
            {
                if (state != EmulationStateName.Finished) return;
                var stats = emulation.StatusMonitor.GenIterationsStatistics;
                var statsInfo = string.Join(", ", stats);
                Console.WriteLine($"[{stats.Count}] {statsInfo}");
            };
            emulation.RunWorkerCompleted += (_, eventArgs) =>
            {
                if (eventArgs.Error != null)
                    throw eventArgs.Error;
            };
            // emulation.Init();
            // emulation.Start();
            RunCli(emulation);
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