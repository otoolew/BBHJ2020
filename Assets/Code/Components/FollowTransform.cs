using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    public Transform FollowTarget { get => followTarget; set => followTarget = value; }

    public void StartFollow(Transform target)
    {
        followTarget = target;
    }
    public void StopFollow()
    {
        followTarget = null;
    }
    private void LateUpdate()
    {
        if (followTarget == null) return;

        transform.position = followTarget.position;
    }
}
