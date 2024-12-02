using UnityEngine;
using UnityEngine.UI;

public class TimelineBar : MonoBehaviour
{
    public Image progressBar;
    public float duration = 120f;
    private float elapsedTime = 0f;

    private void Update()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            progressBar.fillAmount = 1 - (elapsedTime / duration);
        }
        else
        {
            // Trigger the final phase when the bar is empty
            FindAnyObjectByType<TimeManager>().StartBossPhase();
        }
    }
}
