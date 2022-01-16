using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
    [SerializeField] private float moveAmount = 50f;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var time = Time.time;
        var pingPong = Mathf.PingPong(time, 1f);
        var minMax = Mathf.Lerp(-moveAmount, moveAmount, pingPong);

        var titleMove = minMax * Vector3.up;

        transform.position = startPosition + titleMove;
    }
}
