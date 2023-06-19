namespace DTTUnityCommon.DataStructs
{
    public interface IDataNodeCreator
    {
        IMetaData MetaData { get; set; }

        IMetaDataNode<NodeType, MetaDataType> Create<NodeType, MetaDataType>(IMetaDataNode<NodeType, MetaDataType> parent) where MetaDataType : IMetaData;
    }
}

