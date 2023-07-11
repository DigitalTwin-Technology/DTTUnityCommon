//using Mono.Cecil; ??
using UnityEngine;

namespace DTTUnityCore.DataStructs
{
    public interface ICombineRendererBuilder : IDataNodeBuilder
    {
        void AddCombineInstance(Material  material, Mesh mesh, Matrix4x4 transform);
    }
}

