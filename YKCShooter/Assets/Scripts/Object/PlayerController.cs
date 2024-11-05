using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public FixedJoystick joystick;
    public Transform character;

    void Start()
    {

    }
    void LateUpdate()
    {
        if (joystick.IsTouching)
        {
            CharacterRoatate();
        }        
    }

    /// <summary>
    /// 캐릭터 회전
    /// </summary>
    void CharacterRoatate()
    {
        float stickH = joystick.Horizontal;
        float stickV = joystick.Vertical;

        double y = Mathf.Atan2(stickH, stickV) * (180.0 / Math.PI);
        character.rotation = Quaternion.Euler(0, (float)y, 0);
    }
}
