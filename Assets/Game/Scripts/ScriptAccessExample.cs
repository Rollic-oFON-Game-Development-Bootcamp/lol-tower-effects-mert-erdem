using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptAccessExample : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private MeshRenderer meshRenderer => _meshRenderer ?? (_meshRenderer = GetComponent<MeshRenderer>());

    //[SerializeField] private MeshRenderer meshRenderer;

    //private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.material.color = Color.red;
    }

    public void SetMaterial(Material material)
    {
        meshRenderer.material = material;
    }
}
