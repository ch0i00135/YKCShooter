using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{    
    public Transform cameraPivot;
    public Slider cameraDistanceSlider;
    public Slider cameraHeightSlider;

    void Update()
    {
        transform.localPosition = new Vector3(0, 0, cameraDistanceSlider.value);
        cameraPivot.rotation = Quaternion.Euler(cameraHeightSlider.value * 10, 0, 0);
    }
}
