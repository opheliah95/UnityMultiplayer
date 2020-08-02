using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Vector2 maxFollowOffset = Vector2.zero;
    [SerializeField] private Vector2 cameraVelocity = new Vector2(4, 0.5f);
    [SerializeField] private Transform playerTransform = null;
    [SerializeField] private CinemachineVirtualCamera virtualCamera = null;

    private CinemachineTransposer transposer;


    void Start()
    {
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerLook(InputAction.CallbackContext context)
    {
        Vector2 lookAxis = context.ReadValue<Vector2>();
        float followOffset = Mathf.Clamp(transposer.m_FollowOffset.y - cameraVelocity.y * lookAxis.y * Time.deltaTime, maxFollowOffset.x, maxFollowOffset.y);
        transposer.m_FollowOffset.y = followOffset;
        playerTransform.Rotate(0, lookAxis.x * cameraVelocity.x * Time.deltaTime, 0);
    }

   
}
