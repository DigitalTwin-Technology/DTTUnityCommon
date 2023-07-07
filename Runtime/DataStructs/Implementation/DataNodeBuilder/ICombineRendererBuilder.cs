//using Mono.Cecil; ??
using UnityEngine;

namespace DTTUnityCore.DataStructs
{
    public interface ICombineRendererBuilder : IDataNodeBuilder
    {
        void Initialize(int capacity, string name = "Combine Renderers", int layer = 0);

        void AddCombineInstance(Material  material, Mesh mesh, Matrix4x4 transform);
    }
}

