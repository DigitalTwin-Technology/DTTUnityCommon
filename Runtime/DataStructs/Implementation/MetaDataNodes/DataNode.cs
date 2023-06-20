using UnityEngine;
using DTTUnityCommon.DataStructs;
using DTTUnityCommon.Functional;

public class DataNode : DataNodeBase
{
    public Material ReferencedMaterial;

    public void AddCube()
    {
        if (ReferencedMaterial == null)
        {
            return;
        }

        AddNode(new MeshBuilder("Cube", 
            PrimitiveType.Cube, 
            ReferencedMaterial, 
            false), 
            Option<DataNodeBase>.None);
    }

    private void OnGUI()
    {
        if(GUILayout.Button("Add Cube"))
        {
            AddCube();
        }
    }
}
