using DG.Tweening;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Bike : MonoBehaviour
{
    [SerializeField] Vector3 defaultPlayerPosition;

    [Header("�������")]
    [SerializeField] Transform frontWheel;
    public float moveSpeed;             // ������� �̵� �ӵ�
    public float wheelTurnSpeed;        // �չ��� ȸ�� �ӵ�
    public float maxWheelAngle;         // �ִ� �չ��� ȸ�� ����
    public float waitTimeAfterAligned;  // ���� �� �̵� ��� �ð�
    public float waitTimeAfterStart;    // ���� �� �̵� ��� �ð�

    float currentWheelAngle;            // ���� �չ��� ����
    bool isAligned = false;             // Ÿ�ٰ� Z�� ���� ����
    bool canMove = false;               // �̵� ���� ����

    [Header("ĳ����")]
    [SerializeField] Transform headBone;
    public float headRotationSpeed;     // �Ӹ� ȸ�� �ӵ�
    public float maxRotateAngle;           // �Ӹ� ȸ�� ���� ����

    Quaternion initialHeadRotation;     // �ʱ� �Ӹ� ����
    

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
