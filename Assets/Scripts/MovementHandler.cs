using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    public CharacterController CharController;
    public Transform cam;

    [SerializeField] float MovementSpeed = 6;
    [SerializeField] float JumpHeight = 5;

    private bool isGrounded;

    // Movement Variables
    private float worldGravity;

    private Vector3 playerVelocity;

    private float smoothTurnTime = 0.1f;
    private float smoothTurnVelocity;

    private float horizontal;
    private float vertical;
    private Vector3 direction;

    private float directionAngle;
    private float dampAngle;

    private void Start()
    {
        worldGravity = Physics.gravity.y - 15;
    }

    private void Update()
    {


        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        isGrounded = CharController.isGrounded;

        direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude > 0.1f)
        {
            directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            dampAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, directionAngle, ref smoothTurnVelocity, smoothTurnTime);
            transform.rotation = Quaternion.Euler(0f, dampAngle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, directionAngle, 0f) * Vector3.forward;
            CharController.Move(moveDir * MovementSpeed * Time.deltaTime);
        }

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerVelocity.y += MathF.Sqrt(JumpHeight * -1.5f * worldGravity);
        }

        playerVelocity.y += worldGravity * Time.deltaTime;
        CharController.Move(playerVelocity * Time.deltaTime);
    }

}
