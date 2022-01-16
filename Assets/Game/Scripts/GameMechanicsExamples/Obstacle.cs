using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week3_1
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private Renderer obstacleRenderer;

        public ObstacleData data;
        private bool isInitialized = false;

        private void Start()
        {
            Initialize();
        }

        public void Initialize(bool isWhackAMole = false)
        {
            if (!isInitialized)
            {
                obstacleRenderer.gameObject.tag = data.ObstacleTag;
                obstacleRenderer.sharedMaterial = data.ObstacleMaterial;
                isInitialized = true;
                if (isWhackAMole)
                {
                    StartCoroutine(WhackAMoleRoutine());
                }
            }
        }

        private IEnumerator WhackAMoleRoutine()
        {
            yield return new WaitForSeconds(2f);

            if (data.ObstacleTag == "DestroyableObstacle")
            {
                GameManager.Instance.IsGameOver = true;
            }
            Destroy(gameObject);
        }

        private void OnHit()
        {

        }
    }
}
