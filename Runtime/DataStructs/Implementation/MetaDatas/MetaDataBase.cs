using System;
using UnityEngine;

namespace DTTUnityCore.DataStructs
{
    [Serializable]
    public class MetaDataBase : IMetaData
    {
        [SerializeField] private string _id;

        public MetaDataBase()
        {
            _id = Guid.NewGuid().ToString();
        }

        public MetaDataBase(string id)
        {
            _id = id;
        }

        public string Id { get => _id;  }
    }

    [Serializable]
    public class MetaDataName : MetaDataBase
    {
        [SerializeField] protected string _name;

        public MetaDataName(string name) : base()
        {
            _name = name;
        }

        public string Name { get => _name; }
    }

    [Serializable]
    public class MetaDataNumber : MetaDataBase
    {
        [SerializeField]
        private int _number;

        public MetaDataNumber(int number) : base(Guid.NewGuid().ToString())
        {
            _number = number;
        }

        public MetaDataNumber(string id, int number) : base(id)
        {
            _number = number;
        }

        public int Number { get => _number; set => _number = value; }
    }
}

