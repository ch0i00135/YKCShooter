using UnityEngine;

public class AspectRatioController : MonoBehaviour
{
    private int lastWidth = 0;
    private int lastHeight = 0;

    void Update()
    {
        var width = Screen.width;
        var height = Screen.height;

        if (lastWidth != width)
        {
            var heightAccordingToWidth = width / 9.0f * 16.0f;
            Screen.SetResolution(width, (int)Mathf.Round(heightAccordingToWidth), false);
        }
        else if (lastHeight != height)
        {
            var widthAccordingToHeight = height / 16.0f * 9.0f;
            Screen.SetResolution((int)Mathf.Round(widthAccordingToHeight), height, false);
        }

        lastWidth = width;
        lastHeight = height;
    }
}