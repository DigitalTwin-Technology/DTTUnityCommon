namespace DTTUnityCommon.DataStructs
{
    public interface IDataNodeCreator
    {
        IMetaDataNode<NodeType, MetaDataType> Create<NodeType, MetaDataType>(IMetaDataNode<NodeType, MetaDataType> parent,
            MetaDataType metaData) where MetaDataType : IMetaData;
    }
}

