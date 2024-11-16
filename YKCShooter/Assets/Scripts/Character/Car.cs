using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Car : MonoBehaviour
{
    [SerializeField] Transform target;

    [Header("��")]
    //[SerializeField] Transform frontWheel;
    public float moveSpeed;             // �� �̵� �ӵ�
    //public float wheelTurnSpeed;        // �չ��� ȸ�� �ӵ�
    //public float maxWheelAngle;         // �ִ� �չ��� ȸ�� ����
    //public float waitTimeAfterAligned;  // ���� �� �̵� ��� �ð�
    //public float waitTimeAfterStart;    // ���� �� �̵� ��� �ð�

    [Header("ĳ����")]
    [SerializeField] Transform spineBone;
    public float bodyRotationSpeed;       // ���� ȸ�� �ӵ�
    public float maxHorizontalAngle;      // ���� ȸ�� ���� ����

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
        // ����� �ٶ󺸴� ȸ�� ���
        Vector3 targetDirection = (target.position - spineBone.position).normalized;

        // ���� ��ǥ�� ������ ���� ȸ��
        Quaternion correctedRotation = Quaternion.LookRotation(
            Vector3.ProjectOnPlane(targetDirection, Vector3.up),  // Y+ ���� ����
            -Vector3.right                                       // X- �� ����
        );

        // ȸ�� ����
        Quaternion additionalRotation = Quaternion.Euler(90, 0, 0);  // X �� ���� 90�� ȸ��

        // ���� ȸ�� ����
        spineBone.rotation = correctedRotation * additionalRotation;
    }    
}
