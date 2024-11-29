using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int initialPoolSize = 10;
    [SerializeField] private bool expandable = true;

    private List<GameObject> pool;

    private void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateNewObject();
        }
    }
    private GameObject CreateNewObject()
    {
        GameObject newObj = Instantiate(prefab, transform);
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