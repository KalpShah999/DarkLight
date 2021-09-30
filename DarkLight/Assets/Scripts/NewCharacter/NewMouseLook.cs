using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMouseLook : MonoBehaviour
{
    public static float mouseSensitivity = 1000f;
    public static int isCameraSwaying;
    public float mouseX;
    public float mouseY; 
    public float cameraSwayAmountForward = 1.5f;
    public float cameraSwayAmountSideway = 3f;
    private float swayAmountx;
    private float swayAmountz; 
    public float swaySmoothFactor = 10f; 
    public Transform playerBody;
    float xRotation = 0f;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void FixedUpdate()
    {
        //Moving the mouse 
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Setting the Sway Amounts for it to be fluid
        if (isCameraSwaying == 1)
        {
            if (swayAmountx < NewPlayerMovement.x - 0.05f)
            {
                swayAmountx += swaySmoothFactor / 10f * Time.deltaTime;
            }
            else if (swayAmountx > NewPlayerMovement.x + 0.05f)
            {
                swayAmountx -= swaySmoothFactor / 10f * Time.deltaTime;
            }
            else
            {
                swayAmountx = NewPlayerMovement.x;
            }
            if (swayAmountz < NewPlayerMovement.z - 0.05f)
            {
                swayAmountz += swaySmoothFactor / 10f * Time.deltaTime;
            }
            else if (swayAmountx > NewPlayerMovement.z + 0.05f)
            {
                swayAmountz -= swaySmoothFactor / 10f * Time.deltaTime;
            }
            else
            {
                swayAmountz = NewPlayerMovement.z;
            }
        }

        //Moving the full character accordingly 
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler((xRotation - swayAmountz * -cameraSwayAmountForward), 0f, (swayAmountx * -cameraSwayAmountSideway));
        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void AdjustMouseSensitivity(float newMouseSensitivity)
    {
        mouseSensitivity = 100f * newMouseSensitivity;
    }

    public void ToggleCameraSway(bool newCameraToggleValue)
    {
        if (newCameraToggleValue)
        {
            isCameraSwaying = 1;
        } else
        {
            isCameraSwaying = 0;
        }
    }
}
