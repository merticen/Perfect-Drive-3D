using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float moveSmoothness;
    public float rotSmoothness;

    public Vector3 moveOffset;
    public Vector3 rotOffset;

    public Transform targetPlayer;
   
    private void FixedUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        HandleMovement();
        HandleRotation();
    }
    void HandleMovement()
    {
        Vector3 targetPos = new Vector3();
        targetPos = targetPlayer.TransformPoint(moveOffset);

        transform.position = Vector3.Lerp(transform.position, targetPos, moveSmoothness * Time.deltaTime);
    }
    void HandleRotation()
    {
        var direction = targetPlayer.position - transform.position;
        var rotation = new Quaternion();

        rotation = Quaternion.LookRotation(direction + rotOffset, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotSmoothness * Time.deltaTime);
    }
    public void ChangeTarget(Transform newTarget)
    {
        targetPlayer = newTarget;
    }
   
}
