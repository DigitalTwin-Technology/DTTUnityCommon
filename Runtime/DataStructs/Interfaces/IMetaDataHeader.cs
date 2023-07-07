// Copyright (c) 2023  DigitalTwin Technology GmbH
// https://www.digitaltwin.technology/

namespace DTTUnityCore.DataStructs
{
    public interface IMetaDataHeader<NodeType, MetaDataType> : IMetaDataNode<NodeType, MetaDataType> where MetaDataType : IMetaData
    {
        IMetaData HeaderData { get; set; }
    }
}

