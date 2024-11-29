using DG.Tweening;
using UnityEngine;

public class EnemyCarMovement : Enemy
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float maxDistance = 10f;
    public float directionChangeChance = 0.01f;

    [Header("Initial Position Settings")]
    public float minZPosition = 15f;
    public float maxZPosition = 30f;

    private float startPosX;
    private int direction = 1;
    private bool canMove = false;
    private float startTime;

    public override void OnEnable()
    {
        base.OnEnable();
        startTime = Time.time;
        startPosX = transform.position.x;
        MoveToRandomPosition();
    }

    void Update()
    {
        if (Time.time - startTime >= 3f && !canMove)
        {
            canMove = true;
        }

        if (canMove)
        {
            MoveSideToSide();
        }
    }

    private void MoveToRandomPosition()
    {
        float randomZ = Random.Range(minZPosition, maxZPosition);
        float randomX = Random.Range(-maxDistance, maxDistance);
        Vector3 newPosition = transform.position;
        newPosition.x = randomX;
        newPosition.z = 45f;
        transform.position = newPosition;
        transform.DOMoveZ(randomZ, 2f);
    }

    private void MoveSideToSide()
    {
        float currentDistance = transform.position.x - startPosX;
        if (Mathf.Abs(currentDistance) >= maxDistance)
        {
            direction *= -1;
        }

        if (Random.value < directionChangeChance)
        {
            direction *= -1;
        }

        Vector3 movement = new Vector3(moveSpeed * direction * Time.deltaTime, 0, 0);
        transform.Translate(movement);
    }

    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.yellow;
            Vector3 leftBound = new Vector3(startPosX - maxDistance, transform.position.y, transform.position.z);
            Vector3 rightBound = new Vector3(startPosX + maxDistance, transform.position.y, transform.position.z);
            Gizmos.DrawLine(leftBound, rightBound);
        }
    }
}