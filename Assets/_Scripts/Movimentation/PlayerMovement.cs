using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed = 7.0f;
    public float runSpeed = 10.0f;
    public float groundDrag = 5.0f;
    public float climbSpeed;    
    public float jumpForce = 12.0f;
    public float jumpCooldown = 0.25f;
    public float airMultiplier = 0.4f;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode runKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight = 2.0f;
    public LayerMask whatIsGround;

    public Transform orientation;
    
    private float horizontalInput, verticalInput;
    public bool grounded;
    private bool readyToJump;
    private Vector3 moveDirection;
    private Rigidbody rb;

    [Header("References")]
    public Climbing climbingScript;
    public PlayerState state;
    public enum PlayerState {
        Walking,
        Running,
        Jumping,
        climbing
    }

    public bool climbing;
    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    private void Update() {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();

        if (grounded) {
            rb.drag = groundDrag;
        } else {
            rb.drag = 0;
        }
    }

    private void FixedUpdate() {
        MovePlayer();
    }


    private void MyInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded) {
            readyToJump = false;
            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void StateHandler() {

        if (grounded && Input.GetKey(runKey)) {
            state = PlayerState.Running;
            moveSpeed = runSpeed;
        } else if (grounded) {
            state = PlayerState.Walking;
            moveSpeed = walkSpeed;
        } else if (climbing) {
            state = PlayerState.climbing;
            moveSpeed = climbSpeed;
        } 
        else {
            state = PlayerState.Jumping;
        }
    }

    private void MovePlayer() {
        if (climbingScript.exitingWall) return;
        
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(grounded) {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force); 
        }
        else if(!grounded) {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl() {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed){
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump() {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    
    private void ResetJump() {
        readyToJump = true;
    }
}
