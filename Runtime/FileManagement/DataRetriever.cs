// Copyright(c) 2023  DigitalTwin Technology GmbH
// https://www.digitaltwin.technology/

using System.Collections.Generic;
using System;

namespace DTTUnityCore.DataRetrieval
{
    public class DataRetriever<T> where T : IDataCountable
    {
        private string DataPath;

        // Consider making the DataRetriever class more flexible by allowing
        // the DataFileReaderFactory to be passed in as a dependency. 

        public DataRetriever(string dataPath)
        {
            DataPath = dataPath;
        }

        public DataFileReader<T> CreateReader<T>(IReaderFactory factory) where T : IDataCountable
        {
            return factory.Create<T>(DataPath);
        }

        public List<DataChartComparable>[] RetrieveData(IReaderFactory factory)
        {
            DataFileReader<T> dataReader = CreateReader<T>(factory);

            if (!dataReader.FileExists(DataPath))
            {
                throw new ArgumentException( "File does not exist at the specified location.");
            }

            dataReader.GetReadings();

            return dataReader.DataArray;
        }

        public Table RetrieveDataTable<T>(IReaderFactory factory) where T : IDataCountable
        {
            DataFileReader<T> dataReader = CreateReader<T>(factory);

            if (!dataReader.FileExists(DataPath))
            {
                throw new ArgumentException("File does not exist at the specified location.");
            }

            dataReader.GetReadings();

            return dataReader.DataTable;
        }
    }
}

