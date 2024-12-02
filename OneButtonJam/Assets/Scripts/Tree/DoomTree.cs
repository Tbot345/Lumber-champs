using System.Collections;
using UnityEngine;

public class DoomTree : MonoBehaviour
{
    public GameObject boss;
    public float bossDuration = 60f;

    private void Start()
    {
        boss.SetActive(false);
    }

    public void StartBoss()
    {
        boss.SetActive(true);
        StartCoroutine(BossCoroutine());
    }

    private IEnumerator BossCoroutine()
    {
        yield return new WaitForSeconds(bossDuration);
        boss.SetActive(false);
    }
}
