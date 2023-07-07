// Copyright (c) 2023  DigitalTwin Technology GmbH
// https://www.digitaltwin.technology/

using System;
using System.Collections.Generic;
using System.Linq;

namespace DTTUnityCore.DataRetrieval
{
    public class Series
    {
        public bool show = true;

        public string name;

        public List<DataChartComparable> values; // size of this array is size of column. Consider list

        public void AddValue(DataChartComparable value)
        {
            if (values == null)
            {
                throw new ArgumentNullException("Series not initialized");
            }

            values.Add(value);
        }

        private List<T> CastSeries<T>()
        {
            try
            {
                return values.Cast<T>().ToList();
            }
            catch (InvalidCastException)
            {
                throw new ArgumentException($"Elements in the series could not be cast to {typeof(T)}.");
            }
        }
    }
}
