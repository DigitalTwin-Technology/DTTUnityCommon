using System;
using UnityEngine;

namespace DTTUnityCommon.DataStructs
{
    [Serializable]
    public class MetaDataPrimitiveMesh : MetaDataName
    {
        [SerializeField] private Vector3 _position;
        [SerializeField] private Quaternion _rotation;
        [SerializeField] private Vector3 _scale;
        [SerializeField] private PrimitiveType _primitiveType;
        [SerializeField] private Material _material;
        [SerializeField] private bool _addMeshCollider;

        public Vector3 Position
        {
            get 
            {
                return _position;
            }
        }

        public Quaternion Rotation
        { 
            get 
            { 
                return _rotation; 
            } 
        }

        public Vector3 Scale
        {
            get
            {
                return _scale;
            }
        }

        public PrimitiveType PrimitiveType { get => _primitiveType; }
        public Material material { get => _material; }

        public bool AddMeshCollider  { get => _addMeshCollider; }

    public MetaDataPrimitiveMesh(string name, PrimitiveType primitiveType, 
        Vector3 position, 
        Quaternion rotation,
        Vector3 scale,
        Material material, 
        bool addCollider = true) : base(name)
        {
            GuardsClauses.ArgumentStringNullOrEmpty(nameof(name), "name");
            GuardsClauses.ArgumentNotNull(nameof(material), "material"); // TODO: Use Option

            _primitiveType = primitiveType;
            _position = position;
            _rotation = rotation;
            _scale = scale;
            _material = material;
            _addMeshCollider = addCollider;
        }
    }
}

