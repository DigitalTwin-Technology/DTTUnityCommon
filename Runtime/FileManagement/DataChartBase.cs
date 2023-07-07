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

    // Consider making DataChartComparable dire the base class

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


    //[Serializable]
    //public class DataNumber : DataChartComparable
    //{
        
    //    private float _number;

    //    public DataNumber(float number) : base(Guid.NewGuid().ToString())
    //    {
    //        _number = number;
    //    }

    //    public DataNumber(string id, float number) : base(id)
    //    {
    //        _number = number;
    //    }

    //    public float Number { get => _number; set => _number = value; }

    //    public override bool Equals(object obj)
    //    {
    //        if (obj == null || !(obj is DataNumber))
    //            return false;
    //        else
    //            return _number == ((DataNumber)obj)._number;
    //    }

    //    public override int CompareTo(object obj)
    //    {
    //        if (obj == null) return 1;

    //        DataNumber other = obj as DataNumber;
    //        if (other != null)
    //            return this._number.CompareTo(other._number);
    //        else
    //            throw new ArgumentException("Object is not a DataNumber");
    //    }
    //}

    //[Serializable]
    //public class DataString : DataChartComparable
    //{
        
    //    private string _string;

    //    public DataString(string constructorString) : base(Guid.NewGuid().ToString())
    //    {
    //        _string = constructorString;
    //    }

    //    public DataString(string id, string constructorString) : base(id)
    //    {
    //        _string = constructorString;
    //    }

    //    public string String { get => _string; set => _string = value; }

    //    public override bool Equals(object obj)
    //    {
    //        if (obj == null || !(obj is DataString))
    //            return false;
    //        else
    //            return _string == ((DataString)obj)._string;
    //    }

    //    public override int GetHashCode()
    //    {
    //        return _string.GetHashCode();
    //    }

    //    public override string ToString()
    //    {
    //        return _string.ToString();
    //    }
    //}

    //[Serializable]
    //public class DataDate : DataChartComparable
    //{
        
    //    private DateTime date; // date is an IComparable

    //    public DataDate(DateTime _date) : base(Guid.NewGuid().ToString())
    //    {
    //        date = _date;
    //    }

    //    public DataDate(string id, DateTime _date) : base(id)
    //    {
    //        date = _date;
    //    }

    //    public DateTime Date { get => date; set => date = value; }

    //    public override bool Equals(object obj)
    //    {
    //        if (obj == null || !(obj is DataNumber))
    //            return false;
    //        else
    //            return date == ((DataDate)obj).date;
    //    }

    //    public override int CompareTo(object obj)
    //    {
    //        if (obj == null) return 1;

    //        DataDate other = obj as DataDate;
    //        if (other != null)
    //            return this.date.CompareTo(other.Date);
    //        else
    //            throw new ArgumentException("Object is not a DataDate");
    //    }
    //}

    //public class DataList<T> : DataChartComparable where T : DataChartComparable
    //{
    //    public List<T> Values { get; set; }

    //    public DataList(List<T> values)
    //    {
    //        Values = values;
    //    }
    //    public override bool Equals(object obj)
    //    {
    //        if (obj == null || !(obj is DataList<T>))
    //            return false;
    //        else
    //            return Equals((DataList<T>)obj);
    //    }
    //}
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
    }

    public class Table
    {
        string name; // name of file
        public List<Series> series = new List<Series>();
        public List<string> categories = new List<string>(); // title for each series. Each category is equal to series.name

        public Table(Type dataType)
        {
            var propertyNames = dataType.GetProperties().Select(property => property.Name).ToList();

            // Initialize the series for each property in the table
            foreach (var propertyName in propertyNames)
            {
                var series = new Series
                {
                    name = propertyName,
                    values = new List<DataChartComparable>()
                };

                this.series.Add(series);
                this.categories.Add(propertyName);
            }
        }
    }
}

