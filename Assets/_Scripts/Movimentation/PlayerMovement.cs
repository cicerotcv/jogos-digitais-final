// O código é baseado em: https://www.youtube.com/watch?v=Ryi9JxbMCFM&list=PLh9SS5jRVLAleXEcDTWxBF39UjyrFc6Nb&index=1

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour {

    private bool isWallRight, isWallLeft;
    private bool isWallRunning;
    private float horizontalInput, verticalInput;
    private bool readyToJump;
    private Vector3 moveDirection;
    private Rigidbody rb;

    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed = 7.0f;
    public float runSpeed = 10.0f;
    public float groundDrag = 5.0f;

    public float jumpForce = 12.0f;
    public float jumpCooldown = 0.25f;
    public float airMultiplier = 0.4f;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode runKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight = 2.0f;
    public LayerMask whatIsGround;

    [Header("Wall Parameters")]
    public LayerMask whatIsWall;
    public float wallrunForce = 3000f;
    public float maxWallrunTime = 2.0f; 
    public float maxWallSpeed = 30.0f;
    public float maxWallRunCameraTilt = 25.0f;
    public float wallRunCameraTilt = 0.0f;

    public bool grounded;
    public Transform orientation;
    [HideInInspector]
    public PlayerState state;
    public enum PlayerState {
        Walking,
        Running,
        Jumping
    }

    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    private void Update() {
        grounded = (Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround) || Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsWall));

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

    // Movimentação do Player
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
        } else {
            state = PlayerState.Jumping;
        }
    }

    private void MovePlayer() {
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

    // Pulo
    private void Jump() {
        if (grounded) {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        if (isWallRunning) {
            Debug.Log("Wall Jump");
        //     if (isWallLeft && !Input.GetKey(KeyCode.D) || isWallRight && !Input.GetKey(KeyCode.A)) {
        //         rb.AddForce(Vector3.up * jumpForce * 1.5f);
        //         rb.AddForce(Vector3.up * jumpForce * 0.5f);
        //     }

        //     if (isWallRight || isWallLeft && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
        //         rb.AddForce(-orientation.up * jumpForce * 1f);
        //     }
        //     if (isWallRight && Input.GetKey(KeyCode.A)) {
        //         rb.AddForce(-orientation.right * jumpForce * 3.2f);
        //     }
        //     if (isWallLeft && Input.GetKey(KeyCode.D)) {
        //         rb.AddForce(orientation.right * jumpForce * 3.2f);
        //     }

        //     rb.AddForce(orientation.forward * jumpForce * 1f);
        }
    }
    
    private void ResetJump() {
        readyToJump = true;
    }
}
