using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;
    public float rotationSpeed;
    public float verticalSensitivity;
    public float horizontalSensitivity;
    float xRotation;
    float yRotation;

    public Transform Focus;

    public CameraStyle currentStyle;

    public enum CameraStyle
    {
        ThirdPerson,
        ShiftedThirdPerson,
        FirstPerson
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


        // Update is called once per frame
    private void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity * Time.deltaTime;
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        if(currentStyle == CameraStyle.ThirdPerson) {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
        if (currentStyle == CameraStyle.FirstPerson)
        {
            //float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity * Time.deltaTime;
            //float mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity * Time.deltaTime;
            //yRotation += mouseX;
            //xRotation -= mouseY;
            //xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            //transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            //orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
        else if (currentStyle == CameraStyle.ShiftedThirdPerson)
        {
            Vector3 dirFocus = Focus.position - new Vector3(transform.position.x, Focus.position.y, transform.position.z);
            orientation.forward = dirFocus.normalized;
            playerObj.forward = dirFocus.normalized;
        }

    }
}