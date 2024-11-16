using UnityEngine;

public class PlayerWeapon : Weapon
{
    [SerializeField] FixedJoystick joystick;

    private void Update()
    {
        if (joystick.IsTouching)
        {
            deltaTimer += Time.deltaTime;
            if (deltaTimer > attackCooldown)
            {
                Attack();
                deltaTimer = 0f;
            }
        }
        else
        {
            deltaTimer = -firstDelay;
        }
    }
}
