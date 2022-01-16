using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Week3_1
{
    public class FloorController : MonoBehaviour
    {
        // Start is called before the first frame update
        IEnumerator Start()
        {
            yield return GameOverRoutine();
        }

        private IEnumerator GameOverRoutine()
        {
            yield return new WaitUntil(() => GameManager.Instance.IsGameOver);
            while (true)
            {
                transform.Rotate(Vector3.up, Time.deltaTime * 360f * 2f);
                yield return null;
            }
        }
    } 
}
