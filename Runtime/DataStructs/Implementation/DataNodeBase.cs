using System.Collections.Generic;
using UnityEngine;

namespace DTTUnityCommon.DataStructs
{

    [System.Serializable]
    public abstract class DataNodeBase : MonoBehaviour, IMetaDataNode<DataNodeBase, IMetaData>
    {
        [SerializeField] private string _id;
        [SerializeField] private DataNodeBase _header;
        [SerializeField] private DataNodeBase _parent;
        [SerializeField] private List<DataNodeBase> _childList = new List<DataNodeBase>();

        [SerializeReference, Atributes.RequireInterface(typeof(IMetaData))]
        private IMetaData _metaData;

      

        #region IMetaDataNode<DataNodeBase, IMetaData> Implementation

        public string Id
        {
            get => _id;
            private set => _id = value;
        }

        public DataNodeBase Header { get => _header; set => _header = value; }

        public DataNodeBase Parent { get => _parent; 
            set 
            {
                _parent = value;
                transform.parent = _parent.transform;
            } 
        }

        [SerializeField]
        public List<DataNodeBase> Childs { get => _childList; set => _childList = value; }

        public IMetaData Data { get => _metaData; set => _metaData = value; }

        public void AddChild(IMetaDataNode<DataNodeBase, IMetaData> newChild)
        {
            newChild.Header = _header;
            newChild.Parent = this;
            _childList.Add((DataNodeBase)newChild);
        }

        public virtual void AddChild(string newChildName, IMetaData metaData)
        {
            DataNodeBase newDataNode = (new GameObject()).AddComponent<DataNode>();
            newDataNode.name = newChildName;
            newDataNode.Data = metaData;

            AddChild(newDataNode);
        }

        public virtual void RemoveChild()
        {
            if (_childList.Count > 0)
            {
                Utilities.SafeDestroy(_childList[^1].gameObject);
                _childList.RemoveAt(_childList.Count - 1);
            }
        }

        public void AddComponent<ComponentType>() where ComponentType : Component
        {
            gameObject.AddComponent<ComponentType>();
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

        public static bool operator == (DataNodeBase person1, DataNodeBase person2)
        {
            if (((object)person1) == null || ((object)person2) == null)
            {
                return System.Object.Equals(person1, person2);
            }


            return person1.Equals(person2);
        }

        public static bool operator != (DataNodeBase person1, DataNodeBase person2)
        {
            if (((object)person1) == null || ((object)person2) == null)
            {
                return !System.Object.Equals(person1, person2);
            }

            return !(person1.Equals(person2));
        }

        #endregion
    }
}

