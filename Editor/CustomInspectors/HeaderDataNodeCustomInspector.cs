﻿// Copyright (c) 2023  DigitalTwin Technology GmbH
// https://www.digitaltwin.technology/

using UnityEditor;
using UnityEngine;
using DTTUnityCore.DataStructs;
using DTTUnityCore.Functional;
using static UnityEngine.UIElements.UxmlAttributeDescription;

[CustomEditor(typeof(HeaderDataNode))]
public class HeaderDataNodeCustomInspector : Editor
{
    HeaderDataNode _target;

    SerializedProperty Childs_Propierty;
    SerializedProperty ReferencedMaterial_Propierty;

    private void OnEnable()
    {
        _target = (HeaderDataNode)target;

        Childs_Propierty = serializedObject.FindProperty("_nodeList");
        ReferencedMaterial_Propierty = serializedObject.FindProperty("ReferencedMaterial");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (_target.Data != null)
        {
            GUILayout.Box("ID: " + ((MetaDataBase)_target.Data).Id, GUILayout.ExpandWidth(true));
        }
        if (ReferencedMaterial_Propierty != null)
        {
            EditorGUILayout.PropertyField(ReferencedMaterial_Propierty);
        }

        if (_target.ReferencedMaterial != null)
        {
            if (GUILayout.Button("Use MaterialPropertyBlocks"))
            {
                _target.ActiveMaterialPropieryBlocks();
            }
            if (GUILayout.Button("Clear MaterialPropertyBlocks"))
            {
                _target.CleatPropieryBlocks();
            }
            if (GUILayout.Button("Add Cube"))
            {
                _target.AddRandomCube(100);
            }
            if (GUILayout.Button("Add Sphere"))
            {
                _target.AddRandomSphere(100);
            }
        }
        EditorGUILayout.Space();


        EditorGUILayout.ObjectField("Header", _target.Header, typeof(DataNodeCubeTest), true);
        if (Childs_Propierty != null)
        {
            EditorGUILayout.PropertyField(Childs_Propierty);
        }
        if (GUILayout.Button("Add Data Node"))
        {
            _target.AddNode("New Data Node", new MetaDataBase(), Option<DataNodeBase>.None);
        }
        if (_target.Childs.Count > 0)
        {
            if (GUILayout.Button("Delete Node"))
            {
                _target.RemoveNode();
            }
            if (GUILayout.Button("Clear Nodes"))
            {
                _target.RemoveAllNodes();
            }
        }
        serializedObject.ApplyModifiedProperties();
    }
}
