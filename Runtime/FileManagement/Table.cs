// Copyright (c) 2023  DigitalTwin Technology GmbH
// https://www.digitaltwin.technology/

using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace DTTUnityCommon.DataRetrieval
{
    public class Table
    {
        string name; // name of file
        public List<Series> series = new List<Series>();
        public List<string> categories = new List<string>(); // title for each series. Each category is equal to series.name

        public Table(Type dataType)
        {
            categories = dataType.GetProperties().Select(property => property.Name).ToList();

            // Initialize the series for each property in the table
            foreach (var categoryName in categories)
            {
                var series = new Series
                {
                    name = categoryName,
                    values = new List<DataChartComparable>()
                };

                this.series.Add(series);
            }
        }
    }
}
