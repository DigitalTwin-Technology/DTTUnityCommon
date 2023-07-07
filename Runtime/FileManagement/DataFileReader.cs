// Copyright(c) 2023  DigitalTwin Technology GmbH
// https://www.digitaltwin.technology/

using System.Collections.Generic;
using System;

namespace DTTUnityCore.DataRetrieval
{
    public abstract class DataFileReader<T> : IFileManager, IDataParser<T>
    {
        public List<T> Data { get; protected set; }

        public List<DataChartComparable>[] DataArray;

        public Table DataTable;

        public virtual void GetReadings()
        {
        }

        public virtual List<T> ParseData(string data)
        {
            throw new NotImplementedException();
        }
        public virtual List<DataChartComparable>[] ParseDataArray(string data)
        {
            throw new NotImplementedException();
        }

        public virtual Table ParseDataTable(string data)
        {
            throw new NotImplementedException();
        }

        public virtual string ReadFile(string filePath)
        {
            return System.IO.File.ReadAllText(filePath);
        }

        public virtual void WriteFile(string filePath, string content)
        {
            System.IO.File.WriteAllText(filePath, content);
        }

        public virtual void DeleteFile(string filePath)
        {
            System.IO.File.Delete(filePath);
        }

        public virtual bool FileExists(string filePath)
        {
            return System.IO.File.Exists(filePath);
        }


    }
}




