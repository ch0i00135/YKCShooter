using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Car : MonoBehaviour
{
    [SerializeField] Vector3 defaultPlayerPosition;

    [Header("��")]
    [SerializeField] Transform carObject;
    public float moveSpeed;               // �� �̵� �ӵ�

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
        Vector3 targetDirection = (defaultPlayerPosition - spineBone.position).normalized;

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
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Tags.T_Bullet))
        {
            other.gameObject.SetActive(false);
        }
    }
}
