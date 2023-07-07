// Copyright(c) 2023  DigitalTwin Technology GmbH
// https://www.digitaltwin.technology/

using System.Collections.Generic;

namespace DTTUnityCore.DataRetrieval
{
        public interface IFileManager
        {
        string ReadFile(string filePath);
        void WriteFile(string filePath, string content);
        void DeleteFile(string filePath);
        bool FileExists(string filePath);

        }

        public interface IDataParser<T>
        {
        List<T> ParseData(string data);
        List<DataChartComparable>[] ParseDataArray(string data);

        Table ParseDataTable(string data);
        }
}


