using UnityEngine;
using UnityEngine.UIElements;

public class EnemyCar : MonoBehaviour
{
    [Header("차")]
    [SerializeField] GameObject car;

    [Header("캐릭터")]
    [SerializeField] Transform spineBone;

    public float spineX;
    public float spineY;
    public float spineZ;

    Transform target;

    private void Start()
    {        
        target = GameManager.Instance.target;
    }
    private void OnEnable()
    {
        target = GameManager.Instance.target;
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
        Quaternion correctedRotation = Quaternion.LookRotation(targetDirection,
           // Vector3.ProjectOnPlane(targetDirection, Vector3.up),  // Y+ 
            -Vector3.right                                       // X- 
        );

        // 회전 보정 적용
        Quaternion additionalRotation = Quaternion.Euler(spineX, spineY, spineZ);

        // 최종 회전 적용
        spineBone.rotation = correctedRotation * additionalRotation;
    }

}
