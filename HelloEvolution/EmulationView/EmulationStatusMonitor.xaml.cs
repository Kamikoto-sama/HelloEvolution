using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using EmulationModel.Models;

namespace EmulationView
{
    public partial class EmulationStatusMonitor : Window
    {
        private readonly StatusMonitor monitor;
        private const int LastIterationsResultsCount = 10;

        public EmulationStatusMonitor(StatusMonitor monitor)
        {
            this.monitor = monitor;
            InitializeComponent();
        }

        public void RenderIterationStats()
        {
            AliveBotsCountLbl.Content = monitor.BotsAliveCount;
            IterationNumberLbl.Content = monitor.GenerationIterationNumber;
        }

        public void RenderGenerationStats()
        {
            GenerationNumberLbl.Content = monitor.GenerationNumber;

            var iterResults = IterationsResults.Children;
            iterResults.Insert(0, new Label
            {
                BorderBrush = Brushes.Gray,
                BorderThickness = new Thickness(1),
                Content = monitor.GenerationIterationNumber
            });
            if (iterResults.Count > LastIterationsResultsCount)
                iterResults.RemoveAt(iterResults.Count - 1);

            SurvivedBots.ItemsSource = monitor.SurvivedBots;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}