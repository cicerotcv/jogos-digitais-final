using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float sensX = 100.0f;
    public float sensY = 100.0f;
    public Transform cameraPosition;
    public Transform player;
    float xRotation, yRotation;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
      float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;  
      float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;  

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraPosition.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        player.transform.Rotate(Vector3.up * mouseX);

    }
}
