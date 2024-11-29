using DG.Tweening;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerWeapon : Weapon
{
    [SerializeField] FixedJoystick joystick;
    [SerializeField] Transform aim;

    [Header("레이저")]
    [SerializeField] LineRenderer lineRenderer;
    public float maxLength = 50f; // 최대 레이저 길이

    public bool isTopViewMode;
    private void Start()
    {
        lineRenderer.positionCount = 2; // 시작점과 끝점
    }

    private void Update()
    {
        if (joystick.IsTouching)
        {
            if(isTopViewMode)transform.LookAt(aim);
            DrawLaser();
            lineRenderer.enabled = true;
            Attack();
        }
        else
        {
            lineRenderer.enabled = false;
            deltaTimer = -firstDelay;
        }
    }

    void DrawLaser()
    {
        Vector3 startPoint = transform.position;
        // 충돌이 없으면 최대 길이만큼 그리기
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, startPoint + transform.forward * maxLength);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(muzzle.position, muzzle.position + muzzle.forward * 100);
    }
}
