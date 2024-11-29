using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    //[SerializeField] Transform camera;
    //[SerializeField] Slider cameraDistanceSlider;
    //[SerializeField] Slider cameraHeightSlider;
    public bool isCameraHigh = true;
    public float moveTime = 0.5f;

    public float hPosZ = 7f;
    public float hRotX = 70f;

    public float lPosZ = 15f;
    public float lRotX = 55f;

    public void SetCamera()
    {
        Debug.Log("SetCamera");
        if (isCameraHigh)
        {
            isCameraHigh = !isCameraHigh;
            transform.DOMoveZ(lPosZ, moveTime);
            transform.DORotate(new Vector3(lRotX, 0, 0), moveTime);
        }
        else
        {
            isCameraHigh = !isCameraHigh;
            transform.DOMoveZ(hPosZ, moveTime);
            transform.DORotate(new Vector3(hRotX, 0, 0), moveTime);
        }
    }
}
