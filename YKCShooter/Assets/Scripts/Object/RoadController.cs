using UnityEngine;

public class RoadController : MonoBehaviour
{
    public Transform[] roads;
    public float moveSpeed;
    void Start()
    {
        
    }

    void LateUpdate()
    {
        foreach (var road in roads)
        {
            moveRoad(road);
        }
    }
    public void moveRoad(Transform road)
    {
        if (road.position.z <= -45) road.position= new Vector3(0, 0, 75);
        road.Translate(Vector3.back*moveSpeed);
    }
}
