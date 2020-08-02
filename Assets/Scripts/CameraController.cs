using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float lookSmooth = 0.1f;
    public Vector3 offsetFromTarget;
    public float xTilt;

    Vector3 destination = Vector3.zero;
    PlayerController player;
    float rotateVel;

    // Start is called before the first frame update
    void Start()
    {
        SetCameraTarget(target);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        // moving
        MoveToTarget();
        // rotating
        LookAtTarget();
    }

    void SetCameraTarget(Transform t)
    {
        target = t;
        if (target != null)
        {
            if (target.GetComponent<PlayerController>())
                player = GetComponent<PlayerController>();
            else
                Debug.LogError("this target needs a player controller");
        }
    }

    void MoveToTarget()
    {
        destination = player.TargetRotation * offsetFromTarget;
        destination += target.position;
        transform.position = destination;
    }

    void LookAtTarget()
    {
    
    }
}
