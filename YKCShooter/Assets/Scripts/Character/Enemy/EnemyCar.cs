using UnityEngine;
using UnityEngine.UIElements;

public class EnemyCar : MonoBehaviour
{
    [Header("��")]
    [SerializeField] GameObject car;

    [Header("ĳ����")]
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
        // ����� �ٶ󺸴� ȸ�� ���
        Vector3 targetDirection = (target.position - spineBone.position).normalized;

        // ���� ��ǥ�� ������ ���� ȸ��
        Quaternion correctedRotation = Quaternion.LookRotation(targetDirection,
           // Vector3.ProjectOnPlane(targetDirection, Vector3.up),  // Y+ 
            -Vector3.right                                       // X- 
        );

        // ȸ�� ���� ����
        Quaternion additionalRotation = Quaternion.Euler(spineX, spineY, spineZ);

        // ���� ȸ�� ����
        spineBone.rotation = correctedRotation * additionalRotation;
    }

}
