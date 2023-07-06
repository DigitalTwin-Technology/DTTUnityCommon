// Copyright (c) 2023  DigitalTwin Technology GmbH
// https://www.digitaltwin.technology/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTTUnityCommon.DataRetrieval
{
    public class Table
    {
        string name; // name of file
        public List<Series> series = new List<Series>();
        public List<string> categories = new List<string>(); // title for each series. Each category is equal to series.name
    }
}
