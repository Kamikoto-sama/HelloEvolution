using System;

namespace EmulationModel.Models
{
    public class Range
    {
        private readonly bool allowNegative;
        public int MaxValue { get; set; }
        public int MinValue { get; set; }

        public Range(int minValue, int maxValue, bool allowNegative = false)
        {
            this.allowNegative = allowNegative;
            MinValue = minValue;
            MaxValue = maxValue;
            ValidateValues();
        }

        private void ValidateValues()
        {
            if (MinValue > MaxValue)
                throw new ArgumentException("Min value is grater than max value");
            if (!allowNegative && (MinValue < 0 || MaxValue < 0))
                throw new ArgumentException("Negative values are not allowed");
        }

        public override string ToString() => $"From {MinValue} to {MaxValue}";
    }
}