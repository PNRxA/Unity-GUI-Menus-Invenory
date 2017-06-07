using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    public float xSpeed, ySpeed, zSpeed;

    // Update is called once per frame
    void Update()
    {
        // Rotate based on speed set in editor
        transform.Rotate(xSpeed, ySpeed, zSpeed);
    }
}
