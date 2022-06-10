// Feito baseado em https://www.youtube.com/watch?v=f473C43s8nE&list=PLh9SS5jRVLAleXEcDTWxBF39UjyrFc6Nb&index=7
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float sensX, sensY;
    public Transform orientation, modelRb;

    private float xRotation, yRotation;
    
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

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        modelRb.rotation = Quaternion.Euler(0, yRotation, 0);

    }
}
