using System.Collections.Generic;
using UnityEngine;

namespace DTTUnityCommon.DataStructs
{

    public interface IMetaDataNode<NodeType, MetaDataType> : IMetaDataNodeBase where MetaDataType : IMetaData
    {
        NodeType Header { get; set; }

        NodeType Parent { get; set; }

        List<NodeType> Childs { get; set; }

        MetaDataType Data { get; set; }

        /// <summary>
        /// Add a new node child
        /// </summary>
        /// <param name="newDataNode">New child name</param>
        /// <param name="metaData">Meta data fro the new data</param>
        void AddChild(string newChildName, MetaDataType metaData);

        /// <summary>
        /// Remove the last child
        /// </summary>
        void RemoveChild();

        void AddComponent<ComponentType>() where ComponentType : Component;

        ComponentType GetComponent<ComponentType>() where ComponentType : Component;

        void RemoveComponent<ComponentType>() where ComponentType : Component;
    }

    public interface IMetaDataHeader<T,M> : IMetaDataNode<T,M> where M : IMetaData
    {
        IMetaData HeaderData { get; set; }
    }
}

