using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform cameraPivot;
    [SerializeField] Slider cameraDistanceSlider;
    [SerializeField] Slider cameraHeightSlider;

    void Update()
    {
        transform.localPosition = new Vector3(0, 0, cameraDistanceSlider.value);
        cameraPivot.rotation = Quaternion.Euler(cameraHeightSlider.value * 10, 0, 0);
    }
}
