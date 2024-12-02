using UnityEngine;

public class BananaEncounter : MonoBehaviour
{
    [SerializeField] private float bananaSpeed = 5f;
    [SerializeField] private float stunDuration = 2f; // Duration of the stun effect
    [SerializeField] private AudioClip bananaAudio;

    private Transform targetTransform;
    private GameObject[] potentialTargets; // Bots and player
    private float distance;

    public Transform spawnerTransform; // The transform of the entity that spawned the banana

    private void Awake()
    {
        // Find all bots and the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject[] bots = GameObject.FindGameObjectsWithTag("Bot");

        // Combine player and bots into a single array
        potentialTargets = new GameObject[bots.Length + 1];
        potentialTargets[0] = player;
        bots.CopyTo(potentialTargets, 1);

        // Filter out the spawner
        if (potentialTargets.Length > 0)
        {
            potentialTargets = System.Array.FindAll(potentialTargets, target => target != null && target.transform != spawnerTransform);

            // Select a random target
            if (potentialTargets.Length > 0)
            {
                int randomTargetIndex = Random.Range(0, potentialTargets.Length);
                targetTransform = potentialTargets[randomTargetIndex].transform;
            }
        }
    }

    private void Update()
    {
        // Only move if there is a valid target
        if (targetTransform != null)
        {
            // Calculate distance and direction
            distance = Vector2.Distance(transform.position, targetTransform.position);
            Vector2 direction = targetTransform.position - transform.position;

            // Move toward the target
            transform.position = Vector2.MoveTowards(transform.position, targetTransform.position, bananaSpeed * Time.deltaTime);

            // Rotate the banana to face the target
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            // Check if banana reaches the target
            if (distance <= 0.1f)
            {
                SFXManager.instance.PlaySFX(bananaAudio);

                ApplyStun();
                Destroy(gameObject);
            }
        }
    }

    private void ApplyStun()
    {
        if (targetTransform != null)
        {
            // Get the Stunnable component and apply the stun
            Stunnable stunnable = targetTransform.GetComponent<Stunnable>();
            if (stunnable != null)
            {
                stunnable.ApplyStun();
            }
        }
    }
}
