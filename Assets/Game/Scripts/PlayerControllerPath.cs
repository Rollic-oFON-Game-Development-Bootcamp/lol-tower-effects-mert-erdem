using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerPath : MonoBehaviour
{
    [SerializeField] private Transform sideMovementRoot;
    [SerializeField] private Transform leftLimit, rightLimit;
    [SerializeField] private float forwardMovementSpeed = 1f, sideMovementSensitivity = 1f, rotationSpeed = 1f;
    [SerializeField] private PerlinPath perlinPath;

    private Vector2 inputDrag;

    private Vector2 previousMousePosition;

    private float leftLimitX => leftLimit.localPosition.x;
    private float rightLimitX => rightLimit.localPosition.x;
    private Vector3 prevPathPoint;

    // Start is called before the first frame update
    void Start()
    {
        transform.DOPath(perlinPath.PathPoints, forwardMovementSpeed, PathType.CatmullRom)
            .OnUpdate(OnPathUpdate)
            .SetSpeedBased()
            .SetEase(Ease.Linear);
    }

    private void OnPathUpdate()
    {
        var direction = (transform.position - prevPathPoint).normalized;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        prevPathPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //HandleForwardMovement();
        HandleInput();
        HandleSideMovement();
    }

    private void HandleForwardMovement()
    {
        transform.Translate(Vector3.forward * forwardMovementSpeed * Time.deltaTime);
    }

    private void HandleSideMovement()
    {
        var localPos = sideMovementRoot.localPosition;
        localPos += Vector3.right * inputDrag.x * sideMovementSensitivity;

        localPos.x = Mathf.Clamp(localPos.x, leftLimitX, rightLimitX);

        sideMovementRoot.localPosition = localPos;
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            var deltaMouse = (Vector2)Input.mousePosition - previousMousePosition;
            inputDrag = deltaMouse;
            previousMousePosition = Input.mousePosition;
        }
        else
        {
            inputDrag = Vector2.zero;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    var rb = other.attachedRigidbody;
    //    rb.isKinematic = false;
    //    other.isTrigger = false;
    //    rb.velocity = transform.forward * 50f;
    //}
}
