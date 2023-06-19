using Mono.Cecil;
using UnityEngine;

namespace DTTUnityCommon.DataStructs
{
    public class DataNodeBuilderEmpty : IDataNodeCreator
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

    public class DataMeshBuilder : IDataNodeCreator
    {
        protected MetaDataPrimitiveMesh _metaDataPrimitiveMesh;

        public DataMeshBuilder(string name, PrimitiveType primitiveType, Color primitiveColor)
        {
            _metaDataPrimitiveMesh = new MetaDataPrimitiveMesh(name, primitiveType, primitiveColor);
        }

        public IMetaData MetaData { get => _metaDataPrimitiveMesh; set => _metaDataPrimitiveMesh = (MetaDataPrimitiveMesh)value; }
        public IMetaDataNode<NodeType, MetaDataType> Create<NodeType, MetaDataType>(IMetaDataNode<NodeType, MetaDataType> parent) where MetaDataType : IMetaData
        {
            DataNodeBase newDataNode = GameObject.CreatePrimitive(_metaDataPrimitiveMesh.PrimitiveType).AddComponent<DataNode>();
            newDataNode.Data = _metaDataPrimitiveMesh;

            newDataNode.name = _metaDataPrimitiveMesh.Name;
            newDataNode.GetComponent<MeshRenderer>().sharedMaterial.color = _metaDataPrimitiveMesh.Color;

            return (IMetaDataNode<NodeType, MetaDataType>)newDataNode;
        }
    }

    public class DataRandomMeshBuilder : IDataNodeCreator
    {
        protected MetaDataPrimitiveMesh _metaDataPrimitiveMesh;

        public DataRandomMeshBuilder(string name, Color primitiveColor) 
        {
            PrimitiveType primitiveType = PrimitiveType.Sphere;
            int randomPrimitive = Random.Range(0, 4);
            switch(randomPrimitive)
            {
                case 0:
                    primitiveType = PrimitiveType.Sphere;
                    break;
                case 1:
                    primitiveType = PrimitiveType.Capsule;
                    break;
                case 2:
                    primitiveType = PrimitiveType.Cylinder;
                    break;
                case 3:
                    primitiveType = PrimitiveType.Cube;
                    break;
            }
            _metaDataPrimitiveMesh = new MetaDataPrimitiveMesh(name, primitiveType, primitiveColor);
        }

        public IMetaData MetaData { get => _metaDataPrimitiveMesh; set => _metaDataPrimitiveMesh = (MetaDataPrimitiveMesh)value; }

        public IMetaDataNode<NodeType, MetaDataType> Create<NodeType, MetaDataType>(IMetaDataNode<NodeType, MetaDataType> parent) where MetaDataType : IMetaData
        {
            DataNodeBase newDataNode = GameObject.CreatePrimitive(_metaDataPrimitiveMesh.PrimitiveType).AddComponent<DataNode>();
            newDataNode.Data = _metaDataPrimitiveMesh;

            newDataNode.name = _metaDataPrimitiveMesh.Name;
            newDataNode.GetComponent<MeshRenderer>().sharedMaterial.color = _metaDataPrimitiveMesh.Color;

            return (IMetaDataNode<NodeType, MetaDataType>)newDataNode;
        }
    }
}

