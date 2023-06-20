
using UnityEditor;
using UnityEngine;
using DTTUnityCommon.DataStructs;
using DTTUnityCommon.Functional;

[CustomEditor(typeof(DataNodeMeshRenderer))]
public class DataNodeMeshRendererInspector : Editor
{
    DataNodeMeshRenderer _target;

    SerializedProperty Childs_Propierty;
    //SerializedProperty ReferencedMaterial_Propierty;

    private void OnEnable()
    {
        _target = (DataNodeMeshRenderer)target;

        Childs_Propierty = serializedObject.FindProperty("_childList");
        //ReferencedMaterial_Propierty = serializedObject.FindProperty("ReferencedMaterial");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawBaseData();
        EditorGUILayout.Space();

        //if (ReferencedMaterial_Propierty != null)
        //{
        //    EditorGUILayout.PropertyField(ReferencedMaterial_Propierty);
        //}

        if (_target.Data != null)
        {
            GUILayout.Box("PrimitiveType: " + ((MetaDataPrimitiveMesh)_target.Data).PrimitiveType, GUILayout.ExpandWidth(true));
            GUILayout.Box("Material: " + ((MetaDataPrimitiveMesh)_target.Data).material, GUILayout.ExpandWidth(true));
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawBaseData()
    {
        if (_target.Data != null)
        {
            GUILayout.Box("ID: " + ((MetaDataPrimitiveMesh)_target.Data).Id, GUILayout.ExpandWidth(true));
        }

        //EditorGUILayout.ObjectField("Header", _target.Header, typeof(DataNode), true);
        if (Childs_Propierty != null)
        {
            EditorGUILayout.PropertyField(Childs_Propierty);
        }

        if (_target.Childs.Count > 0)
        {
            if (GUILayout.Button("Delete Childs"))
            {
                _target.RemoveChild();
            }
        }
    }
}
