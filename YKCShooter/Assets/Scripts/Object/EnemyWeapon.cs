using UnityEngine;

public class EnemyWeapon : Weapon
{
    private void Update()
    {
        deltaTimer += Time.deltaTime;
        if (deltaTimer > attackCooldown)
        {
            Attack();
            deltaTimer = 0f;
        }
    }
}
