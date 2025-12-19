using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    public Animator animator; // controls player animations like (Idle, walk, jump) 
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    // Stores which direction the ground is facing (for slopes)
    Vector3 groundNormal = Vector3.up;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation; // Where "forward" movement is relative to camera

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    [Header("Sound")]
    private AudioSource moveSFX;

    public AudioClip footSteps;
    public AudioClip jump;

    public float volume = 0.7f;

    private float nextStepTime = 0f;
    public float stepRate = 0.5f;       // time between steps

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        moveSFX = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Raycast slightly below the player to check if grounded
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, playerHeight * 0.5f + 0.3f, whatIsGround))
        {
            grounded = true;
            groundNormal = hit.normal; // Remember ground normal for slope movement
        }
        else
        {
            grounded = false;
            groundNormal = Vector3.up;
        }

        MyInput();
        SpeedControl();

        // play footstep sounds
        if (grounded && rb.velocity.magnitude > 0.1f)
        {
            if (Time.time >= nextStepTime)
            {
                if (moveSFX != null && footSteps != null)
                {
                    //Debug.Log("Playing footstep sound");
                    moveSFX.PlayOneShot(footSteps, volume);
                }

                nextStepTime = Time.time + stepRate;
            }
        }

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        if (animator != null)
        {
            // horizontal speed only (ignore vertical velocity)
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            float speed = flatVel.magnitude;

            animator.SetFloat("Speed", speed);
            animator.SetBool("IsGrounded", grounded);
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            // trigger jump animation once when jump starts
            if (animator != null)
                animator.SetTrigger("Jump");

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // calculate raw movement direction from input
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Project movement onto the contact plane so we don't apply forces into walls/steep surfaces
        Vector3 moveDirProjected = Vector3.ProjectOnPlane(moveDirection, groundNormal).normalized;

        if (moveDirProjected == Vector3.zero)
            moveDirProjected = Vector3.zero; // explicit

        // on ground -> use projected direction
        if (grounded)
            rb.AddForce(moveDirProjected * moveSpeed * 10f, ForceMode.Force);
        else // in air, use a lofted control but still avoid forcing into surfaces
            rb.AddForce(Vector3.ProjectOnPlane(moveDirection, Vector3.up).normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        // only clamp horizontal speed, keep vertical as-is
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        //play jump sound
        if (moveSFX != null && jump != null)
        {
            //Debug.Log("Playing jump sound");
            moveSFX.PlayOneShot(jump, volume);
        }
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}
