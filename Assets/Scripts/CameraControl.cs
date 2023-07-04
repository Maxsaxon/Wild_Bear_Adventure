using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Transform target;
    [SerializeField] private Vector3 offsetPosition;
    private bool isPlayerAlive = true;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
    }

    void LateUpdate() // late update is good for any following like camera
    {
        if (isPlayerAlive)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        transform.position = target.TransformPoint(offsetPosition); // transforms from local space to world space
        transform.rotation = target.rotation;
    }

    public void PlayerDied()
    {
        isPlayerAlive = false;
    }
}
