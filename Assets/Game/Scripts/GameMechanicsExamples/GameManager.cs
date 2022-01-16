using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week3_1
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance => instance ??= FindObjectOfType<GameManager>();

        public bool IsGameOver = false;

        private void Awake()
        {
            SetupInstance();
        }

        private void SetupInstance()
        {
            if (Instance == null)
            {
                instance = this;
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }
    } 
}
