using UnityEngine;

namespace DTTUnityCommon.DataStructs
{
    public class DataNodeBuilderEmpty : IDataNodeCreator
    {
        private string _name;

        public DataNodeBuilderEmpty(string name)
        {
            _name = name;
        }

        public IMetaDataNode<NodeType, MetaDataType> Create<NodeType, MetaDataType>(IMetaDataNode<NodeType, MetaDataType> parent,
            MetaDataType metaData) where MetaDataType : IMetaData
        {
            DataNodeBase newDataNode = (new GameObject()).AddComponent<DataNode>();
            newDataNode.name= _name;

            return (IMetaDataNode<NodeType, MetaDataType>)newDataNode;
        }
    }
}

