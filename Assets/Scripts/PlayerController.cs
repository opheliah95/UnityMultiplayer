using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float runSpeed = 5f;
    public float jumpHeight;
    public float rotateSpeed;

    [SerializeField]
    Quaternion targetRotation; // next rotation
    Rigidbody rb;
    [SerializeField]
    float forwardInput, turnInput;
    float inputDelay = 0.1f; // if input value under this value then we dont move
    public Quaternion TargetRotation
    {
        get => targetRotation;
    }


    // Start is called before the fdgirst frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetRotation = transform.rotation;
        forwardInput = turnInput = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Turn();
    }

    private void FixedUpdate()
    {
        StartMovement();
    }

   public void UpdateMovementAxises(InputAction.CallbackContext context)
    {
        forwardInput = context.ReadValue<Vector2>().y;
        turnInput = context.ReadValue<Vector2>().x;
    }

    private void StartMovement()
    {
        rb.velocity = new Vector3(turnInput, 0, forwardInput) * runSpeed;

    }
    
    // turns the player
    void Turn()
    {
        if (Mathf.Abs(turnInput) > inputDelay)
        {
            //move
            targetRotation *= Quaternion.AngleAxis(rotateSpeed * turnInput * Time.deltaTime, Vector3.up);
        }
        
        //transform.rotation = targetRotation;
    }
}
