using DG.Tweening;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Bike : MonoBehaviour
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
    public float maxRotateAngle;           // 머리 회전 각도 제한

    Quaternion initialHeadRotation;     // 초기 머리 각도
    

    void Start()
    {
        initialHeadRotation = headBone.rotation;
    }
    private void OnEnable()
    {
        StartCoroutine(WaitAndMove(waitTimeAfterStart));
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
            //if(zOffset < 0)
            //{
            //    transform.position += transform.forward * moveSpeed/2 * Time.deltaTime;
            //}
            //else if(zOffset > 0)
            //{
            //    transform.position -= transform.forward * moveSpeed * Time.deltaTime;
            //}
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
        //transform.position += direction * moveSpeed * Time.deltaTime;
    }

    IEnumerator WaitAndMove(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canMove = true;
    }
}
