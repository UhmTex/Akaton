using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    public CharacterController CharController;
    public Transform cam;
    public bool PlayerIsDead = false;
    public Animator animator;

    public AudioSource SprintSFX;
    public AudioSource WalkSFX;
    public AudioSource JumpSFX;

    public GameObject SprintParticles;

    [SerializeField] LayerMask GroundLayer;
    [SerializeField] GameObject GroundCheck;
    [SerializeField] float GroundCheckRadius = 1f;

    [SerializeField] float MovementSpeed = 6;
    [SerializeField] float JumpHeight = 5;

    private float sprintBonus = 0;
    private bool isGrounded;

    private bool Jumped;

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
        if (!PlayerIsDead)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            //isGrounded = CharController.isGrounded;
            isGrounded = Physics.CheckSphere(GroundCheck.transform.position, GroundCheckRadius, GroundLayer);

            direction = new Vector3(horizontal, 0, vertical).normalized;

            if (direction.magnitude > 0.1f)
            {
                directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                dampAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, directionAngle, ref smoothTurnVelocity, smoothTurnTime);
                transform.rotation = Quaternion.Euler(0f, dampAngle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, directionAngle, 0f) * Vector3.forward;
                animator.SetBool("isWalking", true);
                
                if (!WalkSFX.isPlaying && !animator.GetBool("isJumping"))
                {
                    WalkSFX.Play();
                }

                CharController.Move(moveDir * (MovementSpeed + sprintBonus) * Time.deltaTime);
            } 
            else
            {
                animator.SetBool("isWalking", false);

                WalkSFX.Stop();
                SprintSFX.Stop();
            }

            if (isGrounded && playerVelocity.y < 0)
            {
                animator.SetBool("isJumping", false);
                playerVelocity.y = -1f;
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                animator.SetTrigger("jumpActivated");
                animator.SetBool("isJumping", true);

                WalkSFX.Stop();
                SprintSFX.Stop();

                if (!JumpSFX.isPlaying)
                {
                    JumpSFX.Play();
                }

                playerVelocity.y += MathF.Sqrt(JumpHeight * -1.5f * worldGravity);
            }

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                if (isGrounded)
                {
                    SprintParticles.SetActive(true);
                } 
                else
                {
                    SprintParticles.SetActive(false);
                }

                animator.SetBool("isSprinting", true);
                WalkSFX.Stop();
                
                if (!SprintSFX.isPlaying && !animator.GetBool("isJumping"))
                {
                    SprintSFX.Play();
                }

                sprintBonus = 5f;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
            {
                SprintParticles.SetActive(false);
                animator.SetBool("isSprinting", false);
                SprintSFX.Stop();
                sprintBonus = 0;
            }

            playerVelocity.y += worldGravity * Time.deltaTime;
            CharController.Move(playerVelocity * Time.deltaTime);
        }
        else
        {
            SprintSFX.Stop();
            WalkSFX.Stop();
            JumpSFX.Stop();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(GroundCheck.transform.position, GroundCheckRadius);
    }
}