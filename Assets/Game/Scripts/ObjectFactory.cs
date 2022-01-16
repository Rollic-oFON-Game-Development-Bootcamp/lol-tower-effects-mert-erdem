using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory : MonoBehaviour
{
    private static ObjectFactory instance;
    public static ObjectFactory Instance => instance ?? (instance = FindObjectOfType<ObjectFactory>());

    [SerializeField] private SpawnObject objectToSpawn;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnObject(Vector2 direction)
    {
        SpawnObject obj = Instantiate(objectToSpawn);
        obj.transform.position = transform.position + (Vector3.up * 3) + new Vector3(direction.x, 0f, direction.y);
    }
}
