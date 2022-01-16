using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime);

    }

    /// <summary>
    /// This is my explanation fo this function.
    /// </summary>
    private void MyMethod()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

    }
}
