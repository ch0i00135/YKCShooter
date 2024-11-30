using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private List<EnemyTypePoolPair> enemyPools = new List<EnemyTypePoolPair>();

    public ObjectPool GetEnemyPool(EnemyType type)
    {
        var poolPair = enemyPools.Find(pair => pair.type == type);
        if (poolPair != null)
        {
            return poolPair.pool;
        }
        Debug.LogError($"Enemy pool not found for type: {type}");
        return null;
    }

    public void GetEnemy(EnemyType type)
    {
        enemyPools[(byte)type].pool.GetObject();
    }
}

[Serializable]
public class EnemyTypePoolPair
{
    public EnemyType type;
    public ObjectPool pool;
}
