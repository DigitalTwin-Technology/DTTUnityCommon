// Copyright (c) 2023  DigitalTwin Technology GmbH
// https://www.digitaltwin.technology/

namespace DTTUnityCore.DataStructs
{
    public interface IDataNodeBuilder
    {
        IMetaData MetaData { get; set; }

        IMetaDataNode<NodeType, MetaDataType> Create<NodeType, MetaDataType>(IMetaDataNode<NodeType, MetaDataType> parent) where MetaDataType : IMetaData;
    }
}

