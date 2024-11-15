using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] FixedJoystick joystick;
    [SerializeField] Transform character;
    [SerializeField] Animator animator;

    void Start()
    {

    }
    void Update()
    {
        if (joystick.IsTouching)
        {
            CharacterRoatate();
            animator.SetBool("IsTouching", true);
        }
        else
        {
            animator.SetBool("IsTouching", false);
        }
    }

    void CharacterRoatate()
    {
        float stickH = joystick.Horizontal;
        float stickV = joystick.Vertical;

        double y = Mathf.Atan2(stickH, stickV) * (180.0 / Math.PI);
        character.rotation = Quaternion.Euler(0, (float)y+10f, 0); // 10µµ º¸Á¤
    }
}
