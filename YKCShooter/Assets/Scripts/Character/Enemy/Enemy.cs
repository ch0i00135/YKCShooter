using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("��")]
    public float HP;
    public float destroyMoveDistance = 20f;
    public float destroyMoveDuration = 1f;
    public Renderer objectRenderer;

    private bool isDying = false;
    private Color originalColor;  // ���� ���� �����
    private ObjectPool explosionPool;

    public virtual void Start()
    {
        originalColor = objectRenderer.material.color;  // �ʱ� ���� ����
        explosionPool = GameObject.Find("Explosion_Pool").GetComponent<ObjectPool>();
    }

    public virtual void OnEnable()
    {
        //ResetColor();
    }

    private void ResetColor()
    {
        if (objectRenderer != null)
        {
            Material mat = objectRenderer.material;
            mat.color = originalColor;
            mat.renderQueue = 2000;  // �⺻ ����ť�� ����
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.T_Bullet))
        {
            other.gameObject.SetActive(false);
            HP -= 1;

            if (HP <= 0 && !isDying)  // isDying üũ �߰�
            {
                isDying = true;  // �÷��� ����
                Die();
            }
        }
    }

    private void Die()
    {
        // ���� ����Ʈ ����
        GameObject effect = explosionPool.GetObject();
        effect.transform.localScale = new Vector3(10f, 10f, 10f);
        effect.transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
        effect.SetActive(true);

        // ���� ������Ʈ�� ������ ���������� ����
        SetTransparentBlack();

        // ���� ���� �� �̵�
        float direction = transform.position.x >= 0 ? 1 : -1;
        float targetX = transform.position.x + (destroyMoveDistance * direction);

        // ���� ������Ʈ�� �̵���Ű�� �Ϸ� �� ��Ȱ��ȭ
        transform.DOMoveX(targetX, destroyMoveDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => {
                gameObject.SetActive(false);
                effect.SetActive(false);
                isDying = false;
            });
    }

    private void SetTransparentBlack()
    {
        if (objectRenderer != null)
        {
            Material mat = objectRenderer.material;
            Color transparentBlack = new Color(0, 0, 0, 0.5f);
            mat.color = transparentBlack;
            mat.renderQueue = 3000;
        }
    }
}
