using DTTUnityCore.DataStructs;
using DTTUnityCore.Functional;
using UnityEngine;

public class DataNodeMeshRenderer : DataNodeBase
{
    private static Material _defaultMaterial;
    private static Material _unlitMaterial;

    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;

    public MeshRenderer MeshRenderer 
    {
        get 
        { 
            if(_meshRenderer == null)
            {
                _meshRenderer = GetComponent<MeshRenderer>();
            }
            return _meshRenderer;
        }
    }

    public MeshFilter MeshFilter
    {
        get
        {
            if (_meshFilter == null)
            {
                _meshFilter = GetComponent<MeshFilter>();
            }
            return _meshFilter;
        }
    }

    public static Material DefaultMaterial 
    {
        get
        {
            if (_defaultMaterial == null)
            {
                _defaultMaterial = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
            }
            return _defaultMaterial;
        }
    }

    public static Material UnlitMaterial
    {
        get
        {
            if (_unlitMaterial == null)
            {
                _unlitMaterial = new Material(Shader.Find("DTT/URP/Unlit"));
            }
            return _unlitMaterial;
        }
    }


    private void Awake()
    {
        _defaultMaterial = new Material(Shader.Find("Universal Render Pipeline/Unlit"));

        _meshRenderer = GetComponent<MeshRenderer>();
        _meshFilter = GetComponent<MeshFilter>();
    }

    public void SetMaterial(Option<Material> material)
    {
        Material defaultMaterial = new Material(Shader.Find(""));
        MeshRenderer.sharedMaterial = material.Match(some => { return some; }, () => DefaultMaterial); 
    }

    public void AddCube()
    {

    }
}
