using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public CountdownTimer countdownTimer;
    public TimelineBar timelineBar;
    [SerializeField] private GameObject leaderboard;
    [SerializeField] private Leaderboard leaderboardScript;

    private void Start()
    {
        countdownTimer.gameObject.SetActive(true);
        timelineBar.gameObject.SetActive(false);
    }

    private void Update()
    {
        leaderboardScript.UpdateLeaderboard();
    }

    public void StartTimelinePhase()
    {
        countdownTimer.gameObject.SetActive(false);
        timelineBar.gameObject.SetActive(true);
    }

    public void StartBossPhase()
    {
        Time.timeScale = 0f;
        leaderboard.gameObject.SetActive(true) ;
    }
}
