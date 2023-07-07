// Copyright (c) 2023  DigitalTwin Technology GmbH
// https://www.digitaltwin.technology/

using UnityEditor;
using UnityEngine;
using DTTUnityCore.DataStructs;
using DTTUnityCore.Functional;

[CustomEditor(typeof(DataNodeCubeTest))]
public class DataNodeCustomInspector : Editor
{
    DataNodeCubeTest _target;

    SerializedProperty Childs_Propierty;

    private void OnEnable()
    {
        _target = (DataNodeCubeTest)target;

        Childs_Propierty = serializedObject.FindProperty("_nodeList");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (_target.Data != null)
        {
            GUILayout.Box("ID: " + ((MetaDataBase)_target.Data).Id, GUILayout.ExpandWidth(true));
        }

        //if (_target.ReferencedMaterial != null)
        //{
        //    if (GUILayout.Button("Add Renderer Child"))
        //    {
        //        _target.AddCube();
        //    }
        //}
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
