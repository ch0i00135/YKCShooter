using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.U2D;

public class Test : MonoBehaviour
{
    public Transform t;
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = t.position - transform.position;
        Gizmos.DrawLine(
            direction,
            direction+transform.forward *5
        );
    }
}
