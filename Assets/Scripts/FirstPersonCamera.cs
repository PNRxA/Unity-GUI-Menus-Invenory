using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{

    public float speedH = 2.0f;
    public float speedV = 2.0f;
    public GameManager gm;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    // Use this for initialization
    void Start()
    {
        HideCursor(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.inMenu)
        {
            Rotation();
        }

        //if (Input.GetKey(KeyCode.Escape))
        //{
        //    // Unhide Cursor
        //    HideCursor(false);
        //}

        //if (Input.GetMouseButtonDown(0))
        //{
        //    HideCursor(true);
        //}
    }

    void Rotation()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        transform.parent.rotation = Quaternion.LookRotation(-Camera.main.transform.forward, Camera.main.transform.up);
        transform.parent.rotation = Quaternion.Euler(new Vector3(0f, transform.parent.rotation.eulerAngles.y, 0f));

    }

    void HideCursor(bool isHiding)
    {
        if (isHiding)
        {
            // Lock and hide cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

}
