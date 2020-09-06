using System;
using EmulationModel;
using EmulationModel.Models;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace EmulationView.ViewModels
{
    public class EmulationConfigViewModel
    {
        private readonly EmulationConfig config;

        public int GenerationSize
        {
            get => config.GenerationSize;
            set => config.GenerationSize = value;
        }

        public int ParentsCount
        {
            get => config.ParentsCount;
            set => config.ParentsCount = value;
        }

        public int MutatedBotsCount
        {
            get => config.MutatedBotsCount;
            set => config.MutatedBotsCount = value;
        }

        public int GenomeSize
        {
            get => config.GenomeSize;
            set => config.GenomeSize = value;
        }

        [ExpandableObject] public Range MutatedGenesCount => config.MutatedGenesCount;

        public int BotInitialHealth
        {
            get => config.BotInitialHealth;
            set => config.BotInitialHealth = value;
        }

        public int BotMaxHealth
        {
            get => config.BotMaxHealth;
            set => config.BotMaxHealth = value;
        }

        public int FoodHealthIncrease
        {
            get => config.FoodHealthIncrease;
            set => config.FoodHealthIncrease = value;
        }

        public string TxtMapFilePath
        {
            get => config.TxtMapFilePath;
            set => config.TxtMapFilePath = value;
        }

        [ExpandableObject] public ItemsSpawnSettings MaxItemsCountOnMap => config.MaxItemsCountOnMap;
        [ExpandableObject] public ItemsSpawnSettings ItemSpawnIterationDelay => config.ItemSpawnIterationDelay;

        public int GenIterationsCountGoal
        {
            get => config.GenIterationsCountGoal;
            set => config.GenIterationsCountGoal = value;
        }

        public DelayTypes DelayType
        {
            get => config.DelayType;
            set => config.DelayType = value;
        }

        public double IterationDelayMilliseconds
        {
            get => config.IterationDelayMilliseconds;
            set => config.IterationDelayMilliseconds = value;
        }

        public EmulationConfigViewModel(EmulationConfig config) => this.config = config;
    }
}