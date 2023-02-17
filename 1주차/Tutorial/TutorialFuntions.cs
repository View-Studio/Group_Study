using UnityEngine;
using UnityEngine.UI;

public class TutorialFuntions : MonoBehaviour
{
    [Space][Space][Space]
    [SerializeField] Light mainLight = null;
    [SerializeField] Light subLight = null;
    [SerializeField] float mainLigth_OffIntensity = 0f;

    public void OffLigth()
    {
        Time.timeScale = 0;
        mainLight.intensity = mainLigth_OffIntensity;
        subLight.intensity = 0.1f;
    }

    public void OnLigth()
    {
        Time.timeScale = 1;
        mainLight.intensity = 1f;
        subLight.intensity = 0.3f;
    }

    public void GameProgress()
    {
        Time.timeScale = 1;
        OnLigth();
    }
}
