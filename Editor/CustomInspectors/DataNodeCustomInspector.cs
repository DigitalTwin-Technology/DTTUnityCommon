
using UnityEditor;
using UnityEngine;
using DTTUnityCommon.DataStructs;
using DTTUnityCommon.Functional;

[CustomEditor(typeof(DataNode))]
public class DataNodeCustomInspector : Editor
{
    DataNode _target;

    SerializedProperty Childs_Propierty;
    SerializedProperty ReferencedMaterial_Propierty;

    private void OnEnable()
    {
        _target = (DataNode)target;

        Childs_Propierty = serializedObject.FindProperty("_nodeList");
        ReferencedMaterial_Propierty = serializedObject.FindProperty("ReferencedMaterial");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawBasicInfo();

        if (GUILayout.Button("Reset MetaData"))
        {
            _target.Data = new MetaDataBase();
            EditorUtility.SetDirty(_target);
        }

        if(GUILayout.Button("Add Empty Child"))
        {
            _target.AddNode("New Data Node", new MetaDataBase(), Option<DataNodeBase>.None);
        }

        if (ReferencedMaterial_Propierty != null)
        {
            EditorGUILayout.PropertyField(ReferencedMaterial_Propierty);
        }

        if (_target.ReferencedMaterial != null) 
        {
            if (GUILayout.Button("Add Renderer Child"))
            {
                _target.AddCube();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    public void DrawBasicInfo()
    {
        if (_target.Data != null)
        {
            GUILayout.Box("ID: " + ((MetaDataBase)_target.Data).Id, GUILayout.ExpandWidth(true));
        }
        EditorGUILayout.Space();

        //EditorGUILayout.ObjectField("Header", _target.Header, typeof(DataNode), true);
        if (Childs_Propierty != null)
        {
            EditorGUILayout.PropertyField(Childs_Propierty);
        }
        if (_target.Childs.Count > 0)
        {
            if (GUILayout.Button("Delete Child"))
            {
                _target.RemoveChild();
            }
        }
    }
}
