using UnityEngine;

namespace DTTUnityCommon.DataStructs
{
    public class DataNodeCreatorEmpty : IDataNodeCreator
    {
        public IMetaDataNode<NodeType, MetaDataType> Create<NodeType, MetaDataType>(IMetaDataNode<NodeType, MetaDataType> parent,
            MetaDataType metaData) where MetaDataType : IMetaData
        {
            DataNodeBase newDataNode = (new GameObject()).AddComponent<DataNode>();

            newDataNode.Header = parent.Header as DataNodeBase;
            newDataNode.Parent = parent as DataNodeBase;
            //parent.AddChild()
            //_childList.Add(dataNode);

            newDataNode.Data = metaData;

            return (IMetaDataNode<NodeType, MetaDataType>)newDataNode;
        }
    }
}

