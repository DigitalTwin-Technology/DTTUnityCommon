// Copyright (c) 2023  DigitalTwin Technology GmbH
// https://www.digitaltwin.technology/

using UnityEngine;
using DTTUnityCore.DataStructs;
using DTTUnityCore.Functional;

public class DataNode : DataNodeBase
{
    private void Reset()
    {
        Data = new MetaDataBase();
    }
}

public class DataNodeCubeTest : DataNodeBase
{
    public Material ReferencedMaterial
    {
        get
        {
            return ((HeaderDataNode)Header).ReferencedMaterial;
        }
    }

    private void Reset()
    {
        Data = new MetaDataBase();
    }

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
}


