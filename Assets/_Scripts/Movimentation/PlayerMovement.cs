using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float runningSpeed = 12f;
    public float groundDrag = 10.0f;
    public float jumpForce = 5.0f;
    public float jumpCooldown = 1.0f;
    public float airMultiplier = 2.5f;
    private bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    [Header("Animation Script")]
    public TwoDimensionalAnimationStateController animationScript;

    private float horizontalInput;
    private float verticalInput;
    private float velocityX;
    private float velocityZ;

    private Vector3 moveDirection;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();
        // SyncSpeedWithAnimation();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    // Funções Personalizadas de movimentação
    void SyncSpeedWithAnimation()
    {
        velocityX = animationScript.velocityX;
        velocityZ = animationScript.velocityZ;
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = this.transform.forward * verticalInput + this.transform.right * horizontalInput;

        // on ground
        if(grounded)
        {
            if (!Input.GetKey("left shift")) {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            }
            else {
                rb.AddForce(moveDirection.normalized * runningSpeed * 10f, ForceMode.Force);
            }
            
        }
        // in air
        else if(!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }


        // float scalingFactor = 2f;

        // float vx = Time.deltaTime * velocityX * scalingFactor;
        // float vz = Time.deltaTime * velocityZ * scalingFactor;

        // transform.position += new Vector3(vx, 0, vz);
    }

    private void SpeedControl()
    {
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
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}
