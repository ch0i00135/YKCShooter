using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.U2D;

public class Test : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector3.up*Time.deltaTime);
    }
}
