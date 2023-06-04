
using DTTUnityCommon.DataStructs;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DataNode))]
public class DataNodeCustomInspector : Editor
{
    DataNode _target;

    SerializedProperty Childs_Propierty;

    string _value;
    int _number;

    private void OnEnable()
    {
        _target = (DataNode)target;

        Childs_Propierty = serializedObject.FindProperty("_childList");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        _value = EditorGUILayout.TextField(new GUIContent("ID"),_value);
        _number = EditorGUILayout.IntField(new GUIContent("Number"), _number);

        if (GUILayout.Button("Set ID"))
        {
            _target.Data = new MetaDataBase(_value);
            EditorUtility.SetDirty(_target);
        }

        if (GUILayout.Button("Set Number"))
        {
            _target.Data = new MetaDataNumber(_value, _number);

            //if(_target.Data.GetType() == typeof(MetaDataNumber))
            //{
            //    Debug.Log("MetaData");
            //}

            EditorUtility.SetDirty(_target);
        }

        if (GUILayout.Button("Random Data"))
        {
            _target.Data = new MetaDataBase();
            EditorUtility.SetDirty(_target);
            //Childs_MetaData.serializedObject.ApplyModifiedProperties();
        }
        
        if (_target.Data != null)
        {
            //GUILayout.Box("DATA: " + ((MetaData)_target.Data).Id);
            GUILayout.Box("DATA: " + _target.Data.Id);
        }

        //if (Childs_MetaData != null)
        //{
        //    EditorGUILayout.PropertyField(Childs_MetaData);
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
            _target.AddChild("New Data Node", new MetaDataBase());
        }

        if(_target.Childs.Count > 0)
        {
            if (GUILayout.Button("Delete Child"))
            {
                _target.RemoveChild();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
