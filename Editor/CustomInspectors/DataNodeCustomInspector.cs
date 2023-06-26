// Copyright (c) 2023  DigitalTwin Technology GmbH
// https://www.digitaltwin.technology/

using UnityEditor;
using UnityEngine;
using DTTUnityCommon.DataStructs;
using DTTUnityCommon.Functional;

[CustomEditor(typeof(DataNode))]
public class DataNodeCustomInspector : Editor
{
    DataNode _target;

    SerializedProperty Childs_Propierty;

    private void OnEnable()
    {
        _target = (DataNode)target;

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

        EditorGUILayout.ObjectField("Header", _target.Header, typeof(DataNode), true);
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