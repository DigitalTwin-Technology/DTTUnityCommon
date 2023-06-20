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

        Vector3 position = 10.0f * Random.insideUnitSphere;    

        AddNode(new MeshBuilder("Cube", 
            PrimitiveType.Cube,
            position,
            Quaternion.identity,
            Vector3.one,
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
