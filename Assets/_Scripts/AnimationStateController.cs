using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;

    int isWalkingHash;

    int isRunningHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    // https://www.youtube.com/watch?v=FF6kezDQZ7s&list=PLwyUzJb_FNeTQwyGujWRLqnfKpV-cj-eO&index=3
    void Update()
    {
        // current states
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        // inputs
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");

        HandleCondition(isWalkingHash, !isWalking && forwardPressed, true);
        HandleCondition(isWalkingHash, isWalking && !forwardPressed, false);
        HandleCondition(isRunningHash, !isRunning && (forwardPressed && runPressed), true);
        HandleCondition(isRunningHash, isRunning && (!forwardPressed || !runPressed), false);
    }

    void HandleCondition(int stateHash, bool condition, bool newValue)
    {
        if (condition) animator.SetBool(stateHash, newValue);
    }
}
