
using System.Collections.Generic;
using UnityEngine;
using DTTUnityCommon.Functional;

namespace DTTUnityCommon.DataStructs
{
    public interface IMetaDataNode<NodeType, MetaDataType> : IMetaDataNodeBase where MetaDataType : IMetaData
    {
        NodeType Header { get; set; }

        List<NodeType> Childs { get; set; }

        MetaDataType Data { get; set; }

        /// <summary>
        /// Add node child node
        /// </summary>
        /// <param name="newChild">New node to add</param>
        void AddNode(IMetaDataNode<NodeType, MetaDataType> newChild);

        /// <summary>
        /// Add a new child node
        /// </summary>
        /// <param name="newDataNode">New child name</param>
        /// <param name="metaData">Meta data fro the new data</param>
        void AddNode(string newChildName, MetaDataType metaData, Option<NodeType> parent);

        /// <summary>
        /// Add a new child node
        /// </summary>
        /// <param name="newDataNode">New child name</param>
        /// <param name="metaData">Meta data fro the new data</param>
        void AddNode(IDataNodeBuilder nodeCreator, Option<DataNodeBase> parent);

        /// <summary>
        /// Remove the last child
        /// </summary>
        void RemoveNode();

        /// <summary>
        /// Remove the last child
        /// </summary>
        void RemoveAllNodes();

        ComponentType AddComponent<ComponentType>() where ComponentType : Component;

        ComponentType GetComponent<ComponentType>() where ComponentType : Component;

        void RemoveComponent<ComponentType>() where ComponentType : Component;
    }
}

