using DG.Tweening;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerWeapon : Weapon
{
    [SerializeField] FixedJoystick joystick;
    [SerializeField] Transform aim;

    [Header("������")]
    [SerializeField] LineRenderer lineRenderer;
    public float maxLength = 50f; // �ִ� ������ ����

    public bool isTopViewMode;
    private void Start()
    {
        lineRenderer.positionCount = 2; // �������� ����
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
        // �浹�� ������ �ִ� ���̸�ŭ �׸���
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, startPoint + transform.forward * maxLength);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(muzzle.position, muzzle.position + muzzle.forward * 100);
    }
}
