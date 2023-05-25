using System.Collections.Generic;
using UnityEngine;

namespace DTTUnityCommon.DataStructs
{

    public interface IMetaDataNode<T> : IMetaDataNodeBase
    {
        T Header { get; set; }

        T Parent { get; set; }

        List<T> Childs { get; set; }

        IMetaData Data { get; set; }

        void AddComponent<ComponentType>() where ComponentType : Component;

        ComponentType GetComponent<ComponentType>() where ComponentType : Component;

        void RemoveComponent<ComponentType>() where ComponentType : Component;
    }

    public interface IMetaDataHeader<T> : IMetaDataNode<T>
    {
        public IMetaData HeaderData { get; set; }
    }
}

