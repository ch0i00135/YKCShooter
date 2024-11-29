using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EnemyBike : Enemy
{
    [SerializeField] Vector3 defaultPlayerPosition;

    [Header("오토바이")]
    [SerializeField] Transform frontWheel;
    public float moveSpeed;             // 오토바이 이동 속도
    public float wheelTurnSpeed;        // 앞바퀴 회전 속도
    public float maxWheelAngle;         // 최대 앞바퀴 회전 각도
    public float waitTimeAfterAligned;  // 정렬 후 이동 대기 시간
    public float waitTimeAfterStart;    // 시작 후 이동 대기 시간

    float currentWheelAngle;            // 현재 앞바퀴 각도
    bool isAligned = false;             // 타겟과 Z축 정렬 상태
    bool canMove = false;               // 이동 가능 상태

    [Header("캐릭터")]
    [SerializeField] Transform headBone;
    public float headRotationSpeed;     // 머리 회전 속도
    public float maxRotateAngle;        // 머리 회전 각도 제한

    Quaternion initialHeadRotation;     // 초기 머리 각도

    public override void OnEnable()
    {
        base.OnEnable();
        initialHeadRotation = headBone.rotation;
        SetInitialPosition();
        MoveToRandomZ();
        StartCoroutine(WaitAndMove(waitTimeAfterStart));
    }

    private void SetInitialPosition()
    {
        // X 위치 랜덤 선택 (-17~-4 또는 4~17)
        float randomX;
        if (Random.value < 0.5f)
        {
            randomX = Random.Range(-17f, -4f);
        }
        else
        {
            randomX = Random.Range(4f, 17f);
        }

        Vector3 newPosition = transform.position;
        newPosition.x = randomX;
        newPosition.z = 45f;  // 초기 Z 위치
        transform.position = newPosition;
    }

    private void MoveToRandomZ()
    {
        float randomZ = Random.Range(7f, 30f);
        transform.DOMoveZ(randomZ, 1f);  // 1초 동안 이동
    }

    void Update()
    {
        RotateHead();
        if (canMove)
        {
            if (!isAligned)
            {
                AlignWithTargetZ();
            }
            else
            {
                RotateAndMove();
            }
        }
    }

    void RotateHead()
    {
        Vector3 targetDirection = defaultPlayerPosition - headBone.position;
        targetDirection.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        float currentYaw = headBone.rotation.eulerAngles.y;
        float targetYaw = targetRotation.eulerAngles.y;

        float angleDifference = Mathf.DeltaAngle(currentYaw, targetYaw);
        float clampedAngleDifference = Mathf.Clamp(angleDifference, -maxRotateAngle, maxRotateAngle);

        Quaternion newRotation = Quaternion.Euler(0, currentYaw + clampedAngleDifference, 0);

        headBone.rotation = Quaternion.Slerp(headBone.rotation, newRotation, Time.deltaTime * headRotationSpeed);

        Vector3 currentEuler = headBone.rotation.eulerAngles;
        Vector3 initialEuler = initialHeadRotation.eulerAngles;
        headBone.rotation = Quaternion.Euler(initialEuler.x, currentEuler.y, initialEuler.z);
    }

    void AlignWithTargetZ()
    {
        float zOffset = transform.position.z-defaultPlayerPosition.z;
        if (Mathf.Abs(zOffset) > 0)
        {
            if (!DOTween.IsTweening(transform))
            {
                transform.DOMoveZ(defaultPlayerPosition.z, Mathf.Abs(zOffset)/10f);
            }
        }
        else
        {
            isAligned = true;
            canMove = false;
            StartCoroutine(WaitAndMove(waitTimeAfterAligned));
        }
    }

    void RotateAndMove()
    {
        Vector3 direction = (defaultPlayerPosition - transform.position).normalized;
        float angleToTarget = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
        float targetWheelAngle = Mathf.Clamp(angleToTarget, -maxWheelAngle, maxWheelAngle);
        currentWheelAngle = Mathf.Lerp(currentWheelAngle, targetWheelAngle, wheelTurnSpeed * Time.deltaTime);
        frontWheel.localRotation = Quaternion.Euler(0, currentWheelAngle, 0);

        transform.Rotate(Vector3.up, targetWheelAngle * Time.deltaTime);
        if(!DOTween.IsTweening(transform))
        {
            transform.DOMove(defaultPlayerPosition, 1.5f);
        }
    }

    IEnumerator WaitAndMove(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canMove = true;
    }
}
