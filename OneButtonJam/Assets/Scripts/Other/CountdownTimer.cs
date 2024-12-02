using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
    public Image countdownImage;
    public Sprite[] countdownSprites; // Array of sprites for 3, 2, 1, and Go!

    private void Start()
    {
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        Time.timeScale = 0f;

        for (int i = 0; i < countdownSprites.Length; i++)
        {
            countdownImage.sprite = countdownSprites[i];
            yield return new WaitForSecondsRealtime(1);
        }

        countdownImage.gameObject.SetActive(false);
        Time.timeScale = 1f;

        FindAnyObjectByType<TimeManager>().StartTimelinePhase();
    }
}
