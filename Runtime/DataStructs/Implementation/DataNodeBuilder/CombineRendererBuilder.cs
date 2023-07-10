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

        public CombineRendererBuilder(string parentName, int capacity = 0, int layer = 0)
        {
            _materialCombineInstanceDictionary = new Dictionary<Material, List<CombineInstance>>(capacity);
            _materialCombineInstanceDictionary.TrimExcess();

            _metaDataName = new MetaDataName(parentName);
            _layer = layer;
        }

        public IMetaDataNode<NodeType, MetaDataType> Create<NodeType, MetaDataType>(IMetaDataNode<NodeType, MetaDataType> parent) where MetaDataType : IMetaData
        {
            DataNodeBase combineRendererBuilder = (new GameObject(_metaDataName.Name)).AddComponent<DataNode>();

            if (_materialCombineInstanceDictionary != null) 
            {
                foreach (KeyValuePair<Material, List<CombineInstance>> keyValuePair in _materialCombineInstanceDictionary)
                {
                    DataNodeBase combineInstanceNode = (new GameObject(keyValuePair.Key.name)).AddComponent<DataNode>();
                    combineInstanceNode.transform.parent = combineRendererBuilder.transform;
                    combineInstanceNode.gameObject.layer = _layer;

                    Mesh combinedMesh = new Mesh();
                    combinedMesh.CombineMeshes(keyValuePair.Value.ToArray());
                    combineInstanceNode.AddComponent<MeshFilter>().sharedMesh = combinedMesh;

                    combineInstanceNode.AddComponent<MeshRenderer>().sharedMaterial = keyValuePair.Key;

                    combineRendererBuilder.AddNode(combineInstanceNode);
                }
            }

            return (IMetaDataNode<NodeType, MetaDataType>)combineRendererBuilder;
        }

        public void Initialize(int capacity, string name = "Combine Renderers", int layer = 0)
        {
            _materialCombineInstanceDictionary = new Dictionary<Material, List<CombineInstance>>(capacity);
            _materialCombineInstanceDictionary.TrimExcess();

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

