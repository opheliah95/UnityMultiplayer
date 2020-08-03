using Mirror.Examples.Basic;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move Settings")]
    public MoveSettings playerMoveSettings;
    [Space]

    [Header("Physics Settings")]
    public PhysSettings playerPhysSettings;
    [Space]

    Rigidbody rb;
    [SerializeField]
    float forwardInput, turnInput;
    float inputDelay = 0.1f; // if input value under this value then we dont move
    bool isJumping = false;
    public Vector3 velocity;
    [System.Serializable]
    public class MoveSettings
    {
        public float runSpeed = 5f;
        public float jumpHeight = 5f;
        public float raycastToGroundDistance = 0.3f; // how far away is it from ground
        public Transform foot;
        public LayerMask ground;
      
    }
    [System.Serializable]
    public class PhysSettings
    {
        public float downAccelForce = 0.75f;
    }

    // Start is called before the fdgirst frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        forwardInput = turnInput = 0;
    }


    private void FixedUpdate()
    {
        //StartMovement();

        Jump();
        //rb.velocity = velocity;
        rb.velocity = transform.TransformDirection(velocity); // fixed the camera rotation bug
    }

    public void UpdateMovementAxises(InputAction.CallbackContext context)
    {
        velocity.z = context.ReadValue<Vector2>().y * playerMoveSettings.runSpeed;
        velocity.x = context.ReadValue<Vector2>().x * playerMoveSettings.runSpeed;
    }

    private void StartMovement()
    {
        rb.velocity = new Vector3(turnInput, 0, forwardInput) * playerMoveSettings.runSpeed;

    }


    // player jump
    public void OnJump(InputAction.CallbackContext context)
    {
        isJumping = (!isJumping && isGrounded());
    }

    void Jump()
    {
        if (isJumping)
        {
            velocity.y = playerMoveSettings.jumpHeight;
        }
        else if (isGrounded())
        {
            velocity.y = 0;
        }
        else
        {
            //decrease y velocity since it is falling
            velocity.y -= playerPhysSettings.downAccelForce;
        }

    }

    // check if player is grounded
    bool isGrounded()
    {
        return Physics.Raycast(playerMoveSettings.foot.position, Vector3.down, playerMoveSettings.raycastToGroundDistance, playerMoveSettings.ground);
    }

    // check distance to ground
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(playerMoveSettings.foot.position,  playerMoveSettings.raycastToGroundDistance);
    }
}
