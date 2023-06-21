// Copyright(c) 2023  DigitalTwin Technology GmbH
// https://www.digitaltwin.technology/

namespace DTTUnityCommon.DataRetrieval
{
    public interface IReaderFactory
    {
        public DataFileReader<T> Create<T>(string dataPath) where T : IDataCountable;

    }
}