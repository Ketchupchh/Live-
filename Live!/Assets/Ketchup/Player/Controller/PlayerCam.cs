using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{

    [TextArea]
    [SerializeField] private string helper = "This should be on your camera GameObject.";

    #region Variables
    [SerializeField] private Transform orientation;
    public Transform camerapos;

    [Header("Mouse")]
    [Tooltip("The sensitivty of the mouse X-Axis.")]
    [SerializeField] private float sensX = 400f;
    private float xRotation;
    [Tooltip("The sensitivty of the mouse Y-Axis.")]
    [SerializeField] private float sensY = 400f;
    private float yRotation;
    #endregion

    private void Update()
    {
        LookAround();
    }

    void LookAround()
    {
        //Mouse Input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Cam rotation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
