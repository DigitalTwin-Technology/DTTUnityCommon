using UnityEngine;

namespace DTTUnityCommon.DataStructs
{
    public class MeshBuilder : IDataNodeBuilder
    {
        protected MetaDataPrimitiveMesh _metaDataPrimitiveMesh;

        public MeshBuilder(string name, PrimitiveType primitiveType, Vector3 position,
        Quaternion rotation,
        Vector3 scale, 
        Material material, 
        bool AddMeshCollider = true)
        {
            GuardsClauses.ArgumentStringNullOrEmpty(name, nameof(primitiveType));
            GuardsClauses.ArgumentNotNull(material, nameof(material));

            _metaDataPrimitiveMesh = new MetaDataPrimitiveMesh(name, primitiveType, 
                position, 
                rotation, 
                scale, 
                material, 
                AddMeshCollider);
        }

        public IMetaData MetaData { get => _metaDataPrimitiveMesh; set => _metaDataPrimitiveMesh = (MetaDataPrimitiveMesh)value; }

        public IMetaDataNode<NodeType, MetaDataType> Create<NodeType, MetaDataType>(IMetaDataNode<NodeType, MetaDataType> parent) where MetaDataType : IMetaData
        {
            DataNodeBase newDataNode = GameObject.CreatePrimitive(_metaDataPrimitiveMesh.PrimitiveType).AddComponent<DataNodeMeshRenderer>();
            newDataNode.Data = _metaDataPrimitiveMesh;

            newDataNode.name = _metaDataPrimitiveMesh.Name;
            newDataNode.GetComponent<MeshRenderer>().sharedMaterial = _metaDataPrimitiveMesh.material;

            newDataNode.transform.position = _metaDataPrimitiveMesh.Position;
            newDataNode.transform.rotation = _metaDataPrimitiveMesh.Rotation;
            newDataNode.transform.localScale = _metaDataPrimitiveMesh.Scale;

            if (!_metaDataPrimitiveMesh.AddMeshCollider)
            {
                Utilities.SafeDestroy(newDataNode.GetComponent<Collider>());
            }

            return (IMetaDataNode<NodeType, MetaDataType>)newDataNode;
        }
    }
}

