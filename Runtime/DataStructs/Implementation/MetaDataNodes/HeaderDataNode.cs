// Copyright (c) 2023  DigitalTwin Technology GmbH
// https://www.digitaltwin.technology/

using UnityEngine;
using DTTUnityCommon.DataStructs;
using DTTUnityCommon.Functional;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class HeaderDataNode : DataNodeBase
{
    public Material ReferencedMaterial;

    private MeshBuilder _meshBuilder;

    private void Awake()
    {
        _meshBuilder = new MeshBuilder("CAPSULA", PrimitiveType.Capsule, Vector3.zero, Quaternion.identity, Vector3.zero, ReferencedMaterial, true);
    }

    private void Reset()
    {
        Data = new MetaDataBase();
        Header = this;
    }

    public void AddRandomCube(int count)
    {
        if (ReferencedMaterial == null)
        {
            return;
        }

        for(int i=0; i<count; i++) 
        {
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
    }

    public void AddRandomSphere(int count)
    {
        if (ReferencedMaterial == null)
        {
            return;
        }

        for (int i = 0; i < count; i++)
        {
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
    }

    public void ActiveMaterialPropieryBlocks()
    {
        List<MeshRenderer> meshRenderers = new List<MeshRenderer>(GetComponentsInChildren<MeshRenderer>());

        for (int i = 0; i < meshRenderers.Count; i++)
        {
            MaterialPropertyBlock materialPropiertyBlock = new MaterialPropertyBlock();
            meshRenderers[i].GetPropertyBlock(materialPropiertyBlock);

            materialPropiertyBlock.SetTexture("_BaseMap", ReferencedMaterial.GetTexture("_BaseMap"));

            meshRenderers[i].SetPropertyBlock(materialPropiertyBlock);
        }
    }

    public void CleatPropieryBlocks()
    {
        List<MeshRenderer> meshRenderers = new List<MeshRenderer>(GetComponentsInChildren<MeshRenderer>());

        for (int i = 0; i < meshRenderers.Count; i++)
        {
            MaterialPropertyBlock materialPropiertyBlock = new MaterialPropertyBlock();
            meshRenderers[i].GetPropertyBlock(materialPropiertyBlock);
            materialPropiertyBlock.Clear();
            meshRenderers[i].SetPropertyBlock(materialPropiertyBlock);

            StaticBatchingUtility.Combine(meshRenderers[i].gameObject);
        }
    }

    private void OnGUI()
    {
        int width = 200;
        int buttonXPosition = Screen.width - width;
        int buttonWidth = width - 5;

        int yPos = 5;

        GUI.TextArea(new Rect(buttonXPosition, yPos, buttonWidth, 25), "Meshes: " + Childs.Count.ToString());

        yPos += 25;
        if (GUI.Button(new Rect(buttonXPosition, yPos, buttonWidth, 25), "Use MaterialPropertyBlocks"))
        {
            ActiveMaterialPropieryBlocks();
        }

        yPos += 25;
        if (GUI.Button(new Rect(buttonXPosition, yPos, buttonWidth, 25), "Clear MaterialPropertyBlocks"))
        {
            CleatPropieryBlocks();
        }

        yPos += 25;
        if (GUI.Button(new Rect(buttonXPosition, yPos, buttonWidth, 25), "Add Cube"))
        {
            AddRandomCube(100);
        }

        yPos += 25;
        if (GUI.Button(new Rect(buttonXPosition, yPos, buttonWidth, 25), "Add Sphere"))
        {
            AddRandomSphere(100);
        }

        yPos += 25;
        if (GUI.Button(new Rect(buttonXPosition, yPos, buttonWidth, 25), "Add Sphere"))
        {
            AddNode((IMetaDataNode<DataNodeBase, IMetaData>)_meshBuilder);
        }
    }
}

