using UnityEngine;

namespace DTTUnityCore.DataStructs
{
    public class DataNodeBuilderBasicNode : IDataNodeBuilder
    {
        private string _name;

        private MetaDataName _metaDataName;

        public DataNodeBuilderBasicNode(string name)
        {
            _metaDataName = new MetaDataName(name);
        }

        public IMetaData MetaData { get => _metaDataName; set => _metaDataName = (MetaDataName)value; }

        public IMetaDataNode<NodeType, MetaDataType> Create<NodeType, MetaDataType>(IMetaDataNode<NodeType, MetaDataType> parent) where MetaDataType : IMetaData
        {
            DataNodeBase newDataNode = (new GameObject()).AddComponent<DataNodeCubeTest>();
            newDataNode.name = _name;

            return (IMetaDataNode<NodeType, MetaDataType>)newDataNode;
        }
    }
}

