using UnityEngine;

public class AimController : MonoBehaviour
{
    public FixedJoystick joystick;
    public float speed;
    Vector3 moveAim;
    void Start()
    {
        moveAim = new Vector3(0, 0, -950);
    }

    public void FixedUpdate()
    {
        transform.position = moveAim;
        float stickH = joystick.Horizontal;
        float stickV = joystick.Vertical;
        //Debug.Log($"H: {stickH}    V: {stickV}");
        if (-15 > transform.position.x) 
        {
            if (stickH > 0) moveAim.x = transform.position.x + stickH * speed;
        }
        else if (transform.position.x > 15)
        {
            if (stickH < 0) moveAim.x = transform.position.x + stickH * speed;
        }
        else
        {
            Debug.Log("passH");
            moveAim.x = transform.position.x + (stickH * speed);
        }

        if (-15 > transform.position.y) 
        {
            if (stickV > 0) moveAim.y = transform.position.y + stickV * speed;
        }
        else if(transform.position.y > 15)
        {
            if (stickV < 0) moveAim.y = transform.position.y + stickV * speed;
        }
        else
        {
            Debug.Log("passV");
            moveAim.y = transform.position.y + (stickV * speed);
        }

    }
}
