
using DTTUnityCommon.DataStructs;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DataNode))]
public class DataNodeCustomInspector : Editor
{
    DataNode _target;

    SerializedProperty Childs_Propierty;

    private void OnEnable()
    {
        _target = (DataNode)target;

        Childs_Propierty = serializedObject.FindProperty("_childList");
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Random Data"))
        {
            _target.SetMetaData(new MetaData());
        }

        //if (((MetaData)_target.Data) != null)
        //if (_target.Data != null)
        //{
        //    //GUILayout.Box("DATA: " + ((MetaData)_target.Data).Id);
        //    GUILayout.Box("DATA: " + _target.Data.Id);

        //}

        EditorGUILayout.Space();

        EditorGUILayout.ObjectField("Header", _target.Header, typeof(DataNode), true);
        EditorGUILayout.ObjectField("Parent", _target.Parent, typeof(DataNode), true);

        if(Childs_Propierty != null ) 
        {
            EditorGUILayout.PropertyField(Childs_Propierty);
        }

        if(GUILayout.Button("Add Child"))
        {
            GameObject gameObject = new GameObject();
            DataNodeBase dataNode = gameObject.AddComponent<DataNode>();

            _target.AddChild(dataNode, new MetaData());
        }
    }
}
