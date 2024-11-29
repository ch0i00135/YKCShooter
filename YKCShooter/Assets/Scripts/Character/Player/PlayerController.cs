using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.U2D;
using UnityEngine.Animations.Rigging;

public class PlayerController : MonoBehaviour
{
    [SerializeField] FixedJoystick joystick;
    [SerializeField] Transform character;
    [SerializeField] Transform spineBone;
    [SerializeField] Transform aimPivot;
    [SerializeField] Transform aim;
    [SerializeField] Animator animator;
    [SerializeField] Collider col;
    [SerializeField] Rig rig;
    

    void Start()
    {
    }
    void Update()
    {
        if (joystick.IsTouching)
        {
            AimRotate();
            CharacterRoatate(); 
            animator.SetBool("IsTouching", true);
            rig.weight = 1.0f;
            col.enabled = true;
        }
        else
        {
            animator.SetBool("IsTouching", false);
            rig.weight = 0f;
            col.enabled = false;
        }
    }

    void CharacterRoatate()
    {
        character.rotation = aimPivot.rotation;
    }
    void AimRotate()
    {
        float stickH = joystick.Horizontal;
        float stickV = joystick.Vertical;

        double y = Mathf.Atan2(stickH, stickV) * (180.0 / Math.PI);
        aimPivot.rotation = Quaternion.Euler(0, (float)y, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.T_Bullet))
        {
            other.gameObject.SetActive(false);
        }
    }
}
