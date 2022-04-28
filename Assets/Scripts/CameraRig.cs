using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{

    public float mouseSensivity = 100f;
    public Vector2 xLimit = new Vector2(-30f, 30f);
    public Vector2 yLimit = new Vector2(-30f, 30f);
    float xRotation = 0f;
    float yRotation = 0f;

    bool mouseHold = false;

    public bool MouseHold { get => mouseHold; set => mouseHold = value; }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(!mouseHold) {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;

            xRotation -= mouseY;
            yRotation += mouseX;
            xRotation = Mathf.Clamp(xRotation, xLimit.x, xLimit.y);
            yRotation = Mathf.Clamp(yRotation, yLimit.x, yLimit.y);

            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }
    }
}
