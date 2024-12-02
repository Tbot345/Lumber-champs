using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    private List<TreeChopping> players;
    public GameObject leaderboardUI;
    public TextMeshProUGUI[] rankTexts; // Array of Text objects for first, second, third, fourth place

    private void Start()
    {
        players = FindObjectsOfType<TreeChopping>().ToList();
        UpdateLeaderboard();
    }

    public void UpdateLeaderboard()
    {
        if (players == null || players.Count == 0) return;

        // Sort players by points in descending order
        players = players.OrderByDescending(p => p.playerPoints).ToList();

        // Update the leaderboard UI with the top 4 players
        for (int i = 0; i < rankTexts.Length && i < players.Count; i++)
        {
            rankTexts[i].text = $"{i + 1}. {players[i].name}: {players[i].playerPoints} points";
        }

        // Disable rank texts if there are fewer players than the number of rank slots
        for (int i = players.Count; i < rankTexts.Length; i++)
        {
            rankTexts[i].gameObject.SetActive(false);
        }
    }

    public void AddPlayer(TreeChopping player)
    {
        if (players == null) players = new List<TreeChopping>();
        players.Add(player);
    }

    public void RemovePlayer(TreeChopping player)
    {
        if (players == null) return;
        players.Remove(player);
    }

    public void ShowLeaderboard()
    {
        UpdateLeaderboard();
        leaderboardUI.SetActive(true);
    }

    public void HideLeaderboard()
    {
        leaderboardUI.SetActive(false);
    }

}
