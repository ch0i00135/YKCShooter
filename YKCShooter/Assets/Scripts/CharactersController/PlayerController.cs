using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public FixedJoystick joystick;
    public Transform character;

    void Start()
    {
        
    }
    void LateUpdate()
    {
        //joystick.SnapX = true;
        //joystick.SnapY = true;

        float stickH = joystick.Horizontal;
        float stickV = joystick.Vertical;

        double y = Mathf.Atan2(stickH, stickV) * (180.0 / Math.PI);
        character.rotation = Quaternion.Euler(0, (float)y, 0);
    }
}
