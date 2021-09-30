using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpead = 20f;
    public float panBorderThickness = 10f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if (Input.GetKey("w"))
        {
            pos.z += panSpead * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.z -= panSpead * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x -= panSpead * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += panSpead * Time.deltaTime;
        }

        transform.position = pos;
    }
}
