// Copyright (c) 2023  DigitalTwin Technology GmbH
// https://www.digitaltwin.technology/

using System;
using System.Collections.Generic;
using UnityEngine;
using DTTUnityCommon.Functional;

namespace DTTUnityCommon.DataStructs
{

    [Serializable]
    public abstract class DataNodeBase : MonoBehaviour, IMetaDataNode<DataNodeBase, IMetaData>
    {
        [SerializeField] private string _id;
        [SerializeField] private DataNodeBase _header;
        [SerializeField] private List<DataNodeBase> _nodeList = new List<DataNodeBase>();

        [SerializeReference, Atributes.RequireInterface(typeof(IMetaData))]
        private IMetaData _metaData;

        #region IMetaDataNode<DataNodeBase, IMetaData> Implementation

        public string Id
        {
            get => _id;
            private set => _id = value;
        }

        public DataNodeBase Header { get => _header; set => _header = value; }

        [SerializeField]
        public List<DataNodeBase> Childs { get => _nodeList; set => _nodeList = value; }

        public IMetaData Data { get => _metaData; set => _metaData = value; }

        public void AddNode(IMetaDataNode<DataNodeBase, IMetaData> newChild)
        {
            _nodeList.Add((DataNodeBase)newChild);
        }

        public void AddNode(string newChildName, IMetaData metaData, Option<DataNodeBase> parent)
        {
            DataNodeBase newDataNode = (new GameObject()).AddComponent<DataNode>();
            newDataNode.name = newChildName;
            newDataNode.Data = metaData;
            newDataNode.Header = _header;

            newDataNode.transform.parent = parent.Match(some => some, () => this).transform;

            AddNode(newDataNode);
        }

        public void AddNode(IDataNodeBuilder nodeCreator, Option<DataNodeBase> parent)
        {
            DataNodeBase newDataNode = (DataNodeBase)nodeCreator.Create(this);
            newDataNode.Header = _header;

            newDataNode.transform.parent = parent.Match(some => some, () => this).transform;

            AddNode(newDataNode);
        }

        public virtual void RemoveNode()
        {
            if (_nodeList.Count > 0)
            {
                Utilities.SafeDestroy(_nodeList[^1].gameObject);
                _nodeList.RemoveAt(_nodeList.Count - 1);
            }
        }

        public void RemoveAllNodes()
        {
            for (int i = _nodeList.Count - 1; i >= 0; i--)
            {
                Utilities.SafeDestroy(_nodeList[i].gameObject);
                _nodeList.RemoveAt(i);
            }
            _nodeList.Clear();
        }

        public ComponentType AddComponent<ComponentType>() where ComponentType : Component
        {
            return gameObject.AddComponent<ComponentType>();
        }

        new public ComponentType GetComponent<ComponentType>() where ComponentType : Component
        {
            return gameObject.GetComponent<ComponentType>();
        }

        public void RemoveComponent<ComponentType>() where ComponentType : Component
        {
            ComponentType component = gameObject.GetComponent<ComponentType>();
            if(component != null) 
            {
                Destroy(component);
            }
        }

        #endregion

        #region IEquatable<IDataEntityBase Implementation

        public override bool Equals(System.Object obj)
        {
            DataNodeBase emp = obj as DataNodeBase;
            if (emp != null)
            {
                return Equals(emp);
            }
            else
            {
                return false;
            }
        }

        public bool Equals(IMetaDataNodeBase other)
        {
            if (other == null)
            {
                return false;
            }

            return (_id == other.Id && _header == ((DataNodeBase)other)._header) ? true : false;
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }

        public static bool operator == (DataNodeBase node1, DataNodeBase node2)
        {
            if (((object)node1) == null || ((object)node2) == null)
            {
                return System.Object.Equals(node1, node2);
            }


            return node1.Equals(node2);
        }

        public static bool operator != (DataNodeBase node1, DataNodeBase node2)
        {
            if (((object)node1) == null || ((object)node2) == null)
            {
                return !Equals(node1, node2);
            }

            return !(node1.Equals(node2));
        }

        #endregion
    }
}

