using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static void LoadSampleScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public static void LoadTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public static void LoadStageRoad()
    {
        SceneManager.LoadScene("Stage_Road");
    }
}
