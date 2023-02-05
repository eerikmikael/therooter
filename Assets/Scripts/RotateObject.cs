using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private Rigidbody myBody;

    private void Start()
    {
        myBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // myBody.MoveRotation();
    }
}
