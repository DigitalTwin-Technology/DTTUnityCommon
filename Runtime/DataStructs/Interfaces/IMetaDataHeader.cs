namespace DTTUnityCommon.DataStructs
{
    public interface IMetaDataHeader<NodeType, MetaDataType> : IMetaDataNode<NodeType, MetaDataType> where MetaDataType : IMetaData
    {
        IMetaData HeaderData { get; set; }
    }
}

