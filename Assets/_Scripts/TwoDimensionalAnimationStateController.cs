using UnityEngine;

public class TwoDimensionalAnimationStateController : MonoBehaviour
{
    Animator animator;

    float velocityX = 0f;

    float velocityZ = 0f;

    int VelocityXHash;

    int VelocityZHash;

    int IsJumpingHash;

    [Header("Threshold Parameters")]
    public float maxWalkVelocity = 0.5f;

    public float maxRunVelocity = 2f;

    public float tolerance = 0.05f;

    [Header("Acceleration Parameters")]
    public float acceleration = 2f;

    public float deceleration = 4f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        VelocityXHash = Animator.StringToHash("Velocity X");
        VelocityZHash = Animator.StringToHash("Velocity Z");
        IsJumpingHash = Animator.StringToHash("Is Jumping");
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool backwardPressed = Input.GetKey(KeyCode.S);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        bool jumpPressed = Input.GetKey(KeyCode.Space);

        float currentMaxVelocity =
            runPressed ? maxRunVelocity : maxWalkVelocity;

        HandleMovementX (
            currentMaxVelocity,
            runPressed,
            leftPressed,
            rightPressed
        );
        HandleMovementZ (
            currentMaxVelocity,
            runPressed,
            backwardPressed,
            forwardPressed
        );

        animator.SetFloat (VelocityXHash, velocityX);
        animator.SetFloat (VelocityZHash, velocityZ);
        animator.SetBool (IsJumpingHash, jumpPressed);
    }

    void HandleMovementZ(
        float currentMaxVelocity,
        bool runPressed,
        bool backwardPressed,
        bool forwardPressed
    )
    {
        if (forwardPressed)
        {
            if (velocityZ < currentMaxVelocity)
            {
                velocityZ += Time.deltaTime * acceleration;
            }

            if (runPressed && velocityZ > currentMaxVelocity)
            {
                velocityZ = currentMaxVelocity;
            }
            else if (velocityZ > currentMaxVelocity)
            {
                velocityZ -= Time.deltaTime * deceleration;
                if (
                    velocityZ > currentMaxVelocity &&
                    Calc.Around(velocityZ, currentMaxVelocity, tolerance)
                )
                {
                    velocityZ = currentMaxVelocity;
                }
            }
            else if (Calc.Around(velocityZ, currentMaxVelocity, tolerance))
            {
                velocityZ = currentMaxVelocity;
            }
        }

        if (backwardPressed)
        {
            if (velocityZ > -currentMaxVelocity)
            {
                velocityZ -= Time.deltaTime * acceleration;
            }

            // backward deceleration
            if (runPressed && velocityZ < -currentMaxVelocity)
            {
                velocityZ = -currentMaxVelocity;
            }
            else if (velocityZ < -currentMaxVelocity)
            {
                velocityZ += Time.deltaTime * deceleration;
                if (
                    velocityZ < -currentMaxVelocity &&
                    Calc.Around(velocityZ, -currentMaxVelocity, tolerance)
                )
                {
                    velocityZ = -currentMaxVelocity;
                }
            }
            else if (Calc.Around(velocityZ, -currentMaxVelocity, tolerance))
            {
                velocityZ = -currentMaxVelocity;
            }
        }

        if (!forwardPressed && velocityZ > 0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }

        if (!backwardPressed && velocityZ < 0f)
        {
            velocityZ += Time.deltaTime * deceleration;
        }

        if (
            !backwardPressed &&
            !forwardPressed &&
            velocityZ != 0f &&
            Calc.Around(velocityZ, 0f, tolerance)
        )
        {
            velocityZ = 0f;
        }
    }

    void HandleMovementX(
        float currentMaxVelocity,
        bool runPressed,
        bool leftPressed,
        bool rightPressed
    )
    {
        if (rightPressed)
        {
            if (velocityX < currentMaxVelocity)
            {
                velocityX += Time.deltaTime * acceleration;
            }

            if (runPressed && velocityX > currentMaxVelocity)
            {
                velocityX = currentMaxVelocity;
            }
            else if (velocityX > currentMaxVelocity)
            {
                velocityX -= Time.deltaTime * deceleration;
                if (
                    velocityX > currentMaxVelocity &&
                    Calc.Around(velocityX, currentMaxVelocity, tolerance)
                )
                {
                    velocityX = currentMaxVelocity;
                }
            }
            else if (Calc.Around(velocityX, currentMaxVelocity, tolerance))
            {
                velocityX = currentMaxVelocity;
            }
        }

        if (leftPressed)
        {
            if (velocityX > -currentMaxVelocity)
            {
                velocityX -= Time.deltaTime * acceleration;
            }

            // backward deceleration
            if (runPressed && velocityX < -currentMaxVelocity)
            {
                velocityX = -currentMaxVelocity;
            }
            else if (velocityX < -currentMaxVelocity)
            {
                velocityX += Time.deltaTime * deceleration;
                if (
                    velocityX < -currentMaxVelocity &&
                    Calc.Around(velocityX, -currentMaxVelocity, tolerance)
                )
                {
                    velocityX = -currentMaxVelocity;
                }
            }
            else if (Calc.Around(velocityX, -currentMaxVelocity, tolerance))
            {
                velocityX = -currentMaxVelocity;
            }
        }

        if (!rightPressed && velocityX > 0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }

        if (!leftPressed && velocityX < 0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }

        if (
            !leftPressed &&
            !rightPressed &&
            velocityX != 0f &&
            Calc.Around(velocityX, 0f, tolerance)
        )
        {
            velocityX = 0f;
        }
    }
}
