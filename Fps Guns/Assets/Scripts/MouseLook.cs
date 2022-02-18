using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;

    [SerializeField] bool lockCursor = true;

    float cameraPitch = 0.0f;
    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    private float xRotation;


    private void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    // Update is called once per frame
    void Update()
    {

        UpdatedMousePos();
        /*ver2();*/
    }

    #region MouseLookFns
    Vector2 GetMouseInput()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity*Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity*Time.deltaTime ;

        return new Vector2(mouseX,mouseY);
    }

    void UpdatedMousePos()
    {
        Vector2 targetMouseDelta = GetMouseInput();

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y*mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void ver2()
    {
        Vector2 mouseInput = GetMouseInput();

        xRotation -= mouseInput.y;

        xRotation = Mathf.Clamp(cameraPitch, -90.0f, 90f);

       cameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector3.up * mouseInput.x);
    }
    #endregion
}
