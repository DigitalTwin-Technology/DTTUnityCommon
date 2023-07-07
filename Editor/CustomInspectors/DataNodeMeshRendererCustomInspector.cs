using UnityEditor;
using UnityEngine;
using DTTUnityCore.DataStructs;


[CustomEditor(typeof(DataNodeMeshRenderer))]
public class DataNodeMeshRendererCustomInspector : Editor
{
    DataNodeMeshRenderer _target;

    SerializedProperty Childs_Propierty;

    private void OnEnable()
    {
        _target = (DataNodeMeshRenderer)target;

        Childs_Propierty = serializedObject.FindProperty("_childList");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawBaseData();
        EditorGUILayout.Space();

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

        if (Childs_Propierty != null)
        {
            EditorGUILayout.PropertyField(Childs_Propierty);
        }

        if (_target.Childs.Count > 0)
        {
            if (GUILayout.Button("Delete Childs"))
            {
                _target.RemoveNode();
            }
        }
    }
}
