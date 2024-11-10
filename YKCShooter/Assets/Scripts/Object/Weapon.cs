using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Transform muzzle;
    [SerializeField] ObjectPool bulletPool;
    [SerializeField] FixedJoystick joystick;

    private float attackTimer = 0f;

    public float bulletSpeed = 10f;
    public float attackCooldown = 0.3f;
    public float firstDelay = 1f;

    private void FixedUpdate()
    {
        if (joystick.IsTouching)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer > attackCooldown)
            {
               Attack();
                attackTimer = 0f;
            }
        }
        else
        {
            attackTimer = -firstDelay;
        }
    }

    public void Attack()
    {
        GameObject bullet=bulletPool.GetObject();
        bullet.transform.position = muzzle.transform.position;
        bullet.transform.rotation = muzzle.transform.rotation;

        // 필요한 경우 총알에 속도를 부여합니다
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.linearVelocity = -muzzle.forward * bulletSpeed;
        }
    }
}
