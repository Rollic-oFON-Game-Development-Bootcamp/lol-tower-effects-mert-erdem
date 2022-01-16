using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCubeManager : MonoBehaviour
{
    [SerializeField] private int xCubeCount, yCubeCount, zCubeCount;
    [SerializeField] private float distancing;

    [SerializeField] private ColorCube cubePrefab;

    // Start is called before the first frame update
    void Start()
    {
        var spawnPosition = transform.position;

        for (int x = 0; x < xCubeCount; x++)
        {
            for (int y = 0; y < yCubeCount; y++)
            {
                for (int z = 0; z < zCubeCount; z++)
                {
                    var xOffset = x * distancing;
                    var yOffset = y * distancing;
                    var zOffset = z * distancing;

                    var pos = spawnPosition + new Vector3(xOffset, yOffset, zOffset);

                    var cube = Instantiate(cubePrefab, transform);
                    cube.transform.position = pos;
                    //cube.transform.rotation = Quaternion.Euler(
                    //    Random.Range(0f, 360f),
                    //    Random.Range(0f, 360f),
                    //    Random.Range(0f, 360f));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
