using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PerlinPath : MonoBehaviour
{
    [SerializeField] private List<Transform> path;
    [SerializeField] private int nodeCount = 10;
    [SerializeField] private float nodeDistance = 1f;
    [SerializeField] private float distortionRateX = 1f, distortionRateY = 1f;
    [SerializeField] private float perlinMoveSpeedX = 1f;
    [SerializeField] private float perlinMoveSpeedY = 1f;
    [SerializeField] private LineRenderer pathLine;

    private Transform pencil;

    public Vector3[] PathPoints => path.Select(o => o.position).ToArray();

    private void OnDrawGizmos()
    {
        var col = Gizmos.color;
        Gizmos.color = Color.cyan;
        for (int i = 0; i < path.Count; i++)
        {
            var currentNode = path[i];
            var pos = currentNode.position;
            Gizmos.DrawSphere(pos, 0.1f);
            if (i < path.Count - 1)
            {
                var nextNode = path[i + 1];
                var nextPos = nextNode.position;
                Gizmos.DrawLine(pos, nextPos);
            }
        }

        Gizmos.color = col;
    }

    private void Awake()
    {
        pencil = new GameObject("Pencil").transform;
        pencil.SetParent(transform);
        pencil.position = transform.position;
        pencil.rotation = transform.rotation;
        GeneratePath();
    }

    private void GeneratePath()
    {
        var node = CreateNodeFromPencil();
        path.Add(node);

        for (int i = 0; i < nodeCount; i++)
        {
            pencil.position += pencil.forward * nodeDistance;
            //pencil.position += new Vector3(Random.Range(-1f, 1f) * distortionRateX, Random.Range(-1f, 1f) * distortionRateY, 0f);
            node = CreateNodeFromPencil();
            path.Add(node);
        }

        UpdatePathRandom();
    }

    private Transform CreateNodeFromPencil()
    {
        var node = new GameObject().transform;
        node.SetParent(transform);
        node.position = pencil.position;
        node.rotation = pencil.rotation;

        return node;
    }

    private void Update()
    {
        //UpdatePathRandom();
    }

    private void UpdatePathRandom()
    {
        pencil.position = transform.position;
        pencil.rotation = transform.rotation;

        for (int i = 0; i < path.Count; i++)
        {
            var prevPencilPosition = pencil.position;
            if (i > 0)
            {
                pencil.position += pencil.forward * nodeDistance; 
            }

            var perlinX = Mathf.PerlinNoise(perlinMoveSpeedX * i * 0.01f, 0f);
            var perlinY = Mathf.PerlinNoise(0f, perlinMoveSpeedY * i * 0.01f);

            var distortionX = Mathf.Lerp(-1f, 1f, perlinX) * distortionRateX;
            var distortionY = Mathf.Lerp(-1f, 1f, perlinY) * distortionRateY;
            pencil.position = new Vector3(distortionX, distortionY, pencil.position.z);

            var moveDirection = pencil.position - prevPencilPosition;
            moveDirection.Normalize();

            var nodeRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            var node = path[i];
            node.position = pencil.position;
            node.rotation = transform.rotation;

            if (i > 0)
            {
                var prevNode = path[i - 1];
                prevNode.rotation = nodeRotation;
            }
        }

        pathLine.positionCount = path.Count;
        pathLine.SetPositions(PathPoints);

    }
}
