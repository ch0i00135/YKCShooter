using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int initialPoolSize = 10;
    [SerializeField] private bool expandable = true;

    private List<GameObject> pool;
    private Transform poolParent;

    private void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        pool = new List<GameObject>();
        poolParent = new GameObject(prefab.name + " Pool").transform;
        poolParent.SetParent(transform);

        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewObject();
        }
    }

    private GameObject CreateNewObject()
    {
        GameObject newObj = Instantiate(prefab, poolParent);
        newObj.SetActive(false);
        pool.Add(newObj);
        return newObj;
    }

    public GameObject GetObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        if (expandable)
        {
            GameObject newObj = CreateNewObject();
            newObj.SetActive(true);
            return newObj;
        }

        return null;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void ReturnAllObjects()
    {
        foreach (GameObject obj in pool)
        {
            obj.SetActive(false);
        }
    }
}