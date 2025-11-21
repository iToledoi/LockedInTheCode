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

    public GameObject FirstPersonCam;
    public GameObject ThirdPersonCam;
    public GameObject Shifted3rdPersonCam;

    public GameObject crosshairCanvas;

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
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchCameraStyle(CameraStyle.FirstPerson);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchCameraStyle(CameraStyle.ThirdPerson);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchCameraStyle(CameraStyle.ShiftedThirdPerson);


        if (currentStyle == CameraStyle.ThirdPerson)
        {
            crosshairCanvas.SetActive(false);
            Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
            orientation.forward = viewDir.normalized;
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);

        }

        if (currentStyle == CameraStyle.FirstPerson)
        {
            crosshairCanvas.SetActive(true);
            float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity * Time.deltaTime;
            yRotation += mouseX;
            //yRotation = Mathf.Clamp(yRotation, -60f, 60f);
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -40f, 40f);
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
        else if (currentStyle == CameraStyle.ShiftedThirdPerson)
        {
            crosshairCanvas.SetActive(false);
            Vector3 dirFocus = Focus.position - new Vector3(transform.position.x, Focus.position.y, transform.position.z);
            orientation.forward = dirFocus.normalized;
            playerObj.forward = dirFocus.normalized;
        }

    }

    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        currentStyle = newStyle;
        FirstPersonCam.SetActive(false);
        ThirdPersonCam.SetActive(false);
        Shifted3rdPersonCam.SetActive(false);
        switch (newStyle)
        {
            case CameraStyle.FirstPerson:
                FirstPersonCam.SetActive(true);
                break;
            case CameraStyle.ThirdPerson:
                ThirdPersonCam.SetActive(true);
                break;
            case CameraStyle.ShiftedThirdPerson:
                Shifted3rdPersonCam.SetActive(true);
                break;
        }
    }
}