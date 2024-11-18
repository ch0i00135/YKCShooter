using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform muzzle;
    [SerializeField] ObjectPool bulletPool;

    public float bulletSpeed = 10f;
    public float attackCooldown = 0.3f;
    public float firstDelay = 1f;

    protected float deltaTimer = 0f;    

    protected void Attack()
    {
        deltaTimer += Time.deltaTime;
        if (deltaTimer > attackCooldown)
        {
            GameObject bullet = bulletPool.GetObject();
            bullet.transform.position = muzzle.transform.position;
            bullet.transform.rotation = muzzle.transform.rotation;

            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.linearVelocity = muzzle.forward * bulletSpeed;
            }
            deltaTimer = 0f;
        }
        
    }
}
