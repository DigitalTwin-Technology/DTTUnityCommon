// Copyright (c) 2023  DigitalTwin Technology GmbH
// https://www.digitaltwin.technology/

using System;
using System.Linq;
using System.Collections.Generic;

namespace DTTUnityCore.DataRetrieval
{
    [Serializable]
    public class DataChartBase : IDataChart, IEquatable<DataChartBase>
    {
         private string _id;

        public DataChartBase()
        {
            _id = Guid.NewGuid().ToString();
        }

        public DataChartBase(string id)
        {
            _id = id;
        }

        public string Id
        {
            get => _id;
            private set => _id = value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is DataChartBase))
                return false;
            else
                return Equals((DataChartBase)obj);
        }

        public bool Equals(DataChartBase chartDataBase)
        {
            return _id == chartDataBase._id;
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        public override string ToString()
        {
            return _id.ToString();
        }
        public static bool operator ==(DataChartBase chartDataBase1, DataChartBase chartDataBase2)
        {
            if (((object)chartDataBase1) == null || ((object)chartDataBase2) == null)
            {
                return Equals(chartDataBase1, chartDataBase2);
            }


            return chartDataBase1.Equals(chartDataBase2);
        }

        public static bool operator !=(DataChartBase chartDataBase1, DataChartBase chartDataBase2)
        {
            if (((object)chartDataBase1) == null || ((object)chartDataBase2) == null)
            {
                return !Equals(chartDataBase1, chartDataBase2);
            }

            return !chartDataBase1.Equals(chartDataBase2);
        }
    }

    // Consider making DataChartComparable directly the base class

    [Serializable]
    public class DataChartComparable : DataChartBase, IComparable
    {
        public DataChartComparable() : base(Guid.NewGuid().ToString())
        {

        }
        public DataChartComparable(string id) : base(id)
        {        

        }

        public virtual int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }

    // Some example concrete implementations of chart data. DataString is a DataChartBase and DataNumber is a DataChartComparable, and therefore a DataChartBase.

    //public class DataNumber : DataChartComparable

    //public class DataString : DataChartBase

    //public class DataDate : DataChartComparable

    //public class DataList<T> : DataChartComparable where T : DataChartComparable
}

