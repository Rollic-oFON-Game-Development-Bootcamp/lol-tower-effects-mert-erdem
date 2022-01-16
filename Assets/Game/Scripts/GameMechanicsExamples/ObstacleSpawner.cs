using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week3_1
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField, BoxGroup("Spawn With Mouse")] private Camera mainCamera;
        [SerializeField, BoxGroup("Spawn With Mouse")] private Obstacle obstaclePrefab;
        [SerializeField, BoxGroup("Spawn With Mouse")] private ObstacleData placableObstacleData, destroyableObstacleData;
        [SerializeField, BoxGroup("Spawn With Mouse")] private bool willSpawnDestroyable = false;

        [SerializeField, BoxGroup("Spawner Game")] private Transform minSpawnPoint, maxSpawnPoint;

        private void OnDrawGizmos()
        {
            var col = Gizmos.color;

            if (minSpawnPoint != null && maxSpawnPoint != null)
            {
                var gizmoColor = Color.blue;
                gizmoColor.a = 0.3f;
                Gizmos.color = gizmoColor;

                var center = Vector3.Lerp(minSpawnPoint.position, maxSpawnPoint.position, 0.5f);
                var size = maxSpawnPoint.position - minSpawnPoint.position;
                size.x = Mathf.Abs(size.x);
                size.y = Mathf.Abs(size.y);
                size.z = Mathf.Abs(size.z);
                Gizmos.DrawCube(center, size); 
            }

            Gizmos.color = col;
        }

        private void Start()
        {
            StartSpawningObstacles();
        }

        // Update is called once per frame
        void Update()
        {
            //HandleInput();
        }

        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var pos = FindWorldPos(Input.mousePosition);

                if (pos.HasValue)
                {
                    SpawnObstacle(pos.Value);
                }
            }
        }

        private Vector3? FindWorldPos(Vector2 inputCoords)
        {
            var ray = mainCamera.ScreenPointToRay(inputCoords);
            Vector3? worldPos = null;

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, LayerMask.GetMask("Floor")))
            {
                worldPos = hitInfo.point;
            }

            return worldPos;
        }

        private void SpawnObstacle(Vector3 pos)
        {
            var obstacle = Instantiate(obstaclePrefab);
            obstacle.transform.position = pos;
            obstacle.data = willSpawnDestroyable ? destroyableObstacleData : placableObstacleData;
            obstacle.Initialize();
        }

        private void StartSpawningObstacles()
        {
            StartCoroutine(AutoSpawnRoutine());
        }

        private IEnumerator AutoSpawnRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(2f);

                if (GameManager.Instance.IsGameOver)
                {
                    yield break;
                }

                var data = Random.value < 0.5f ? destroyableObstacleData : placableObstacleData;
                var minPosition = minSpawnPoint.position;
                var maxPosition = maxSpawnPoint.position;

                var randomPoint = Vector3.zero;
                randomPoint.x = Mathf.Lerp(minPosition.x, maxPosition.x, Random.value);
                randomPoint.y = Mathf.Lerp(minPosition.y, maxPosition.y, Random.value);
                randomPoint.z = Mathf.Lerp(minPosition.z, maxPosition.z, Random.value);

                SpawnObstacle(randomPoint, data);
            }
        }

        private void SpawnObstacle(Vector3 pos, ObstacleData data)
        {
            var obstacle = Instantiate(obstaclePrefab);
            obstacle.transform.position = pos;
            obstacle.data = data;
            obstacle.Initialize(true);
        }
    }
}
