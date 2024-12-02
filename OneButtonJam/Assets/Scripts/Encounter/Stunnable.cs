using UnityEngine;

public class Stunnable : MonoBehaviour
{
    private bool isStunned = false;
    private float stunDuration = 3f; // Duration for how long the stun will last
    private TreeChopping treeChopping;

    private void Start()
    {
        treeChopping = GetComponent<TreeChopping>(); // Reference to TreeChopping script
    }

    public void ApplyStun()
    {
        if (isStunned) return; // Don't apply the stun if already stunned

        isStunned = true;
        treeChopping.enabled = false; // Disable chopping while stunned

        Debug.Log($"{gameObject.name} is stunned!");

        // Re-enable TreeChopping after the stun duration
        Invoke("EndStun", stunDuration);
    }

    private void EndStun()
    {
        isStunned = false;
        treeChopping.enabled = true; // Re-enable chopping after stun duration
        Debug.Log($"{gameObject.name} is no longer stunned!");
    }

    public bool IsStunned()
    {
        return isStunned;
    }
}
