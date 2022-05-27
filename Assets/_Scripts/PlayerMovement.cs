using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float velocityX;

    float velocityZ;

    [Header("Camera Control")]
    float rotationY = 0f;

    [Header("Camera Sensibility")]
    public float sensX = 400f;

    Vector3 moveDirection;

    [Header("Animation Script")]
    public TwoDimensionalAnimationStateController animationScript;

    void Start()
    {
    }

    void Update()
    {
        SyncSpeedWithAnimation();
    }

    void FixedUpdate()
    {
        UpdatePosition();
        UpdateOrietation();
    }

    void UpdateOrietation()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        rotationY += mouseX;
        transform.rotation = Quaternion.Euler(0, rotationY, 0);
    }

    void UpdatePosition()
    {
        float scalingFactor = 2f;

        float vx = Time.deltaTime * velocityX * scalingFactor;
        float vz = Time.deltaTime * velocityZ * scalingFactor;

        transform.position += new Vector3(vx, 0, vz);
    }

    void SyncSpeedWithAnimation()
    {
        velocityX = animationScript.velocityX;
        velocityZ = animationScript.velocityZ;
    }
}
