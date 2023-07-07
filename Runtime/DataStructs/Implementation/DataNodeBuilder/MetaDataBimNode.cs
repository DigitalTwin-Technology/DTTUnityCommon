//using Mono.Cecil; ??
using System;
using UnityEngine;

namespace DTTUnityCore.DataStructs
{
    [Serializable]
    public class MetaDataBimNode : MetaDataBase
    {
        [SerializeField] protected uint _nodeIndex;

        public MetaDataBimNode(uint nodeIndex, string id) : base(id)
        {
            _nodeIndex = nodeIndex;
        }

        public uint NodeIndex { get => _nodeIndex; }
    }
}

