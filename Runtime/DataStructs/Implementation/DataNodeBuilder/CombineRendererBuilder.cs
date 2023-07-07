//using Mono.Cecil; ??
using System.Collections.Generic;
using UnityEngine;

namespace DTTUnityCore.DataStructs
{
    public class CombineRendererBuilder : ICombineRendererBuilder
    {
        private MetaDataName _metaDataName;
        private int _layer;
        private Dictionary<Material, List<CombineInstance>> _materialCombineInstanceDictionary;

        public IMetaData MetaData { get => _metaDataName; set => _metaDataName = (MetaDataName)value; }

        public IMetaDataNode<NodeType, MetaDataType> Create<NodeType, MetaDataType>(IMetaDataNode<NodeType, MetaDataType> parent) where MetaDataType : IMetaData
        {
            DataNodeBase newDataNode = (new GameObject(_metaDataName.Name)).AddComponent<DataNode>();

            if (_materialCombineInstanceDictionary != null) 
            {
                foreach (KeyValuePair<Material, List<CombineInstance>> keyValuePair in _materialCombineInstanceDictionary)
                {
                    GameObject combineInstanceGameObject = new GameObject(keyValuePair.Key.name.ToString());
                    combineInstanceGameObject.transform.parent = newDataNode.transform;
                    combineInstanceGameObject.layer = _layer;

                    Mesh combinedMesh = new Mesh();
                    combinedMesh.CombineMeshes(keyValuePair.Value.ToArray());
                    combineInstanceGameObject.AddComponent<MeshFilter>().sharedMesh = combinedMesh;

                    combineInstanceGameObject.AddComponent<MeshRenderer>().sharedMaterial = keyValuePair.Key;
                }
            }

            return (IMetaDataNode<NodeType, MetaDataType>)newDataNode;
        }

        public void Initialize(int capacity, string name = "Combine Renderers", int layer = 0)
        {
            _materialCombineInstanceDictionary = new Dictionary<Material, List<CombineInstance>>();
            _metaDataName = new MetaDataName(name);
            _layer = layer;
        }

        public void AddCombineInstance(Material material, Mesh mesh, Matrix4x4 transform)
        {
            if (_materialCombineInstanceDictionary.ContainsKey(material))
            {
                _materialCombineInstanceDictionary[material].Add(new CombineInstance()
                {
                    mesh = mesh,
                    transform = transform
                });
            }
            else
            {
                _materialCombineInstanceDictionary[material] = new List<CombineInstance>()
                {
                    new CombineInstance()
                    {
                        mesh = mesh,
                        transform = transform
                    }
                };
            }
        }
    }

    

   
}

