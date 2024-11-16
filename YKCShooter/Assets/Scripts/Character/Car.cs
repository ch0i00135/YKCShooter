using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Car : MonoBehaviour
{
    [SerializeField] Transform target;

    [Header("차")]
    //[SerializeField] Transform frontWheel;
    public float moveSpeed;             // 차 이동 속도
    //public float wheelTurnSpeed;        // 앞바퀴 회전 속도
    //public float maxWheelAngle;         // 최대 앞바퀴 회전 각도
    //public float waitTimeAfterAligned;  // 정렬 후 이동 대기 시간
    //public float waitTimeAfterStart;    // 시작 후 이동 대기 시간

    [Header("캐릭터")]
    [SerializeField] Transform spineBone;
    public float bodyRotationSpeed;       // 몸통 회전 속도
    public float maxHorizontalAngle;      // 몸통 회전 각도 제한

    Quaternion initialBodyRotation;

    void Start()
    {
        initialBodyRotation = spineBone.rotation;
    }
    void Update()
    {
        RotateBody();
    }
    public void RotateBody()
    {
        // 대상을 바라보는 회전 계산
        Vector3 targetDirection = (target.position - spineBone.position).normalized;

        // 로컬 좌표계 보정을 위한 회전
        Quaternion correctedRotation = Quaternion.LookRotation(
            Vector3.ProjectOnPlane(targetDirection, Vector3.up),  // Y+ 정면 기준
            -Vector3.right                                       // X- 업 벡터
        );

        // 회전 적용
        Quaternion additionalRotation = Quaternion.Euler(90, 0, 0);  // X 축 기준 90도 회전

        // 최종 회전 적용
        spineBone.rotation = correctedRotation * additionalRotation;
    }    
}
