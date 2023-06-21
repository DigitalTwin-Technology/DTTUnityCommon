// Copyright (c) 2023  DigitalTwin Technology GmbH
// https://www.digitaltwin.technology/

using UnityEngine;
using DTTUnityCommon.DataStructs;
using DTTUnityCommon.Functional;

public class HeaderDataNode : DataNodeBase
{
    public Material ReferencedMaterial;

    private void Reset()
    {
        Data = new MetaDataBase();
        Header = this;
    }

    public void AddRandomCube()
    {
        if (ReferencedMaterial == null)
        {
            return;
        }

        Vector3 position = 10.0f * Random.insideUnitSphere;

        AddNode(new MeshBuilder("Cube " + (Childs.Count + 1).ToString(),
            PrimitiveType.Cube,
            position,
            Quaternion.identity,
            Vector3.one,
            ReferencedMaterial,
            false),
            Option<DataNodeBase>.None);
    }

    public void AddRandomSphere()
    {
        if (ReferencedMaterial == null)
        {
            return;
        }

        Vector3 position = 10.0f * Random.insideUnitSphere;

        AddNode(new MeshBuilder("Sphere " + (Childs.Count + 1).ToString(),
            PrimitiveType.Sphere,
            position,
            Quaternion.identity,
            Vector3.one,
            ReferencedMaterial,
            false),
            Option<DataNodeBase>.None);
    }

    private void OnGUI()
    {
        int width = 120;
        int buttonXPosition = Screen.width - width;
        int buttonWidth = width - 5;

        if (GUI.Button(new Rect(buttonXPosition, 5, buttonWidth, 25), "Add Cube"))
        {
            AddRandomCube();
        }
        if (GUI.Button(new Rect(buttonXPosition, 30, buttonWidth, 25), "Add Sphere"))
        {
            AddRandomSphere();
        }
    }
}

