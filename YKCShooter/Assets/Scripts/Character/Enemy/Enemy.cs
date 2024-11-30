using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("적")]
    public float HP;
    public float destroyMoveDistance = 20f;
    public float destroyMoveDuration = 1f;
    public Renderer objectRenderer;

    private bool isDying = false;
    private Color originalColor;  // 원래 색상 저장용
    private ObjectPool explosionPool;

    public virtual void Start()
    {
        originalColor = objectRenderer.material.color;  // 초기 색상 저장
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
            mat.renderQueue = 2000;  // 기본 렌더큐로 복구
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.T_Bullet))
        {
            other.gameObject.SetActive(false);
            HP -= 1;

            if (HP <= 0 && !isDying)  // isDying 체크 추가
            {
                isDying = true;  // 플래그 설정
                Die();
            }
        }
    }

    private void Die()
    {
        // 폭발 이펙트 생성
        GameObject effect = explosionPool.GetObject();
        effect.transform.localScale = new Vector3(10f, 10f, 10f);
        effect.transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
        effect.SetActive(true);

        // 현재 오브젝트를 검은색 반투명으로 변경
        SetTransparentBlack();

        // 방향 결정 및 이동
        float direction = transform.position.x >= 0 ? 1 : -1;
        float targetX = transform.position.x + (destroyMoveDistance * direction);

        // 현재 오브젝트를 이동시키고 완료 후 비활성화
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
