using UnityEngine;

public class BotBehaviour : MonoBehaviour
{
    [SerializeField] private TreeChopping treeChopping;
    private bool canChop;
    private Stunnable stunnable;

    private void Awake()
    {
        stunnable = gameObject.GetComponent<Stunnable>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(ChopTree), 0.15f, Random.Range(0.15f, 0.3f)); // Bots chop trees every second
    }

    private void ChopTree()
    {
            if (stunnable != null && stunnable.IsStunned())
            {
                return;
            }

            if (treeChopping.TreeInSight())
            {
                treeChopping.Chop();
            }
    }
}
