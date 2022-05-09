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

    bool noClip = false;

    public bool MouseHold { get => mouseHold; set => mouseHold = value; }

    private Transform defaultState;
    // Start is called before the first frame update
    void Start()
    {
        defaultState = this.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.AltGr)) {
            noClip = !noClip;
        }

        if(noClip) {
            xRotation -= mouseY * 0.9f;
            yRotation += mouseX * 0.9f;
            xRotation = Mathf.Clamp(xRotation, -180, 180);
            yRotation = Mathf.Clamp(yRotation, -360, 360);
            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

            if(Input.GetKey(KeyCode.Z))
                transform.position += Camera.main.transform.forward * Time.deltaTime * 3f;
            if(Input.GetKey(KeyCode.S))
                transform.position -= Camera.main.transform.forward * Time.deltaTime * 3f;
            if(Input.GetKey(KeyCode.Q))
                transform.Translate(-transform.right * Time.deltaTime * 3f);
            if(Input.GetKey(KeyCode.D))
                transform.Translate(transform.right * Time.deltaTime * 3f);
            if(Input.GetKey(KeyCode.Space))
                transform.Translate(Vector3.up * Time.deltaTime * 3f);
            if(Input.GetKey(KeyCode.LeftControl))
                transform.Translate(-Vector3.up * Time.deltaTime * 3f);
        } else {
            if(!mouseHold) {
                xRotation -= mouseY;
                yRotation += mouseX;
                xRotation = Mathf.Clamp(xRotation, xLimit.x, xLimit.y);
                yRotation = Mathf.Clamp(yRotation, yLimit.x, yLimit.y);
                transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

            }
        }
        
    }
}
