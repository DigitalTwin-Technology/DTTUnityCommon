using Mono.Cecil;
using UnityEngine;

namespace DTTUnityCommon.DataStructs
{
    public class DataNodeBuilderEmpty : IDataNodeBuilder
    {
        private string _name;

        private MetaDataName _metaDataName;

        public DataNodeBuilderEmpty(string name)
        {
            _metaDataName = new MetaDataName(name);
        }

        public IMetaData MetaData { get => _metaDataName; set => _metaDataName = (MetaDataName)value; }

        public IMetaDataNode<NodeType, MetaDataType> Create<NodeType, MetaDataType>(IMetaDataNode<NodeType, MetaDataType> parent) where MetaDataType : IMetaData
        {
            DataNodeBase newDataNode = (new GameObject()).AddComponent<DataNode>();
            newDataNode.name = _name;

            return (IMetaDataNode<NodeType, MetaDataType>)newDataNode;
        }
    }
}

