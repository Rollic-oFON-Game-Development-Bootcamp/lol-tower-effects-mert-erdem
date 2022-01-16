using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScriptAccessExample sae;
    [SerializeField] private Material mat;

    public int MyInt;

    // Start is called before the first frame update
    void Start()
    {
        sae.SetMaterial(mat);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
