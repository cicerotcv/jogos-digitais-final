// Esse script foi baseado em:  https://www.youtube.com/watch?v=tAJLiOEfbQg&list=PLh9SS5jRVLAleXEcDTWxBF39UjyrFc6Nb&index=12

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour {
    [Header("References")]
    public PlayerMovement pm;
    public Transform orientation;
    public Rigidbody rb;
    public LayerMask whatIsWall;

    [Header("Climbing")]
    public float climbSpeed = 10.0f;
    public float maxClimbTime = 0.75f;
    private float climbTimer;

    private bool climbing;

    [Header("Detection")]
    public float detectionLength = 0.7f;
    public float sphereCastRadius = 0.25f;
    public float maxWallLookAngle = 30.0f;
    private float wallLookAngle;
    private RaycastHit frontWallHit;
    private bool wallFront;


    private void Update()
    {
        WallCheck();
        StateMachine();

        if (climbing) {
            ClimbingMovement();
        }
    }

    private void StateMachine() {
        if (wallFront && Input.GetKey(KeyCode.W) && wallLookAngle < maxWallLookAngle) {
            if (!climbing && climbTimer > 0) {
                StartClimbing();
            }

            if (climbTimer > 0) {
                climbTimer -= Time.deltaTime;
            } else {
                StopClimbing();
            }
        }
        else {
            if (climbing) {
                StopClimbing();
            }
        }
    }

    private void WallCheck() {
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, whatIsWall);
        wallLookAngle = Vector3.Angle(orientation.forward, -frontWallHit.normal);

        if (pm.grounded) {
            climbTimer = maxClimbTime;
        }
    }

    private void StartClimbing() {
        climbing = true;
    }

    private void ClimbingMovement() {
        rb.velocity = new Vector3(rb.velocity.x, climbSpeed, rb.velocity.z);
    }

    private void StopClimbing() {
        climbing = false;
    }
}
