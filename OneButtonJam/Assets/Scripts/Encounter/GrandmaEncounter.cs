using UnityEngine;

public class GrandmaEncounter : MonoBehaviour
{
    [Header("Grandma Properties")]
    [SerializeField] private float speed;
    [SerializeField] private float stopTime = 3.5f;
    [SerializeField] private float lifetime = 8.5f;

    [Header("Grandma AnimationData")]
    [SerializeField] private AnimationData idle;

    private CustomAnimator animator;
    private GameObject[] players;
    private float stopTimeCounter;
    private int randomSpot;
    private bool destinationReached;

    private void Start()
    {
        stopTimeCounter = stopTime;
        randomSpot = Random.Range(1, 4);
    }

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject[] bots = GameObject.FindGameObjectsWithTag("Bot");

        players = new GameObject[bots.Length + 1];
        players[0] = player;
        bots.CopyTo(players, 1);
        animator = GetComponent<CustomAnimator>();

        animator.AddAnimation(idle.animationName, idle);
        animator.SetAnimation(idle.animationName);
    }


    private void Update()
    {
        if(lifetime >= 0)
        {
            lifetime -= Time.deltaTime;
        } else
        {
            Destroy(gameObject);
        }

        if(destinationReached == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, players[randomSpot].transform.position, speed * Time.deltaTime);
        }

        if(Vector2.Distance(transform.position, players[randomSpot].transform.position) < 0.4f)
        {
            destinationReached = true;

            if(stopTimeCounter <= 0)
            {
                players[randomSpot].GetComponent<TreeChopping>().isGrandmaNear = true;
                randomSpot = Random.Range(0, players.Length);
                stopTimeCounter = stopTime;
            } else
            {
                stopTimeCounter -= Time.deltaTime;
            }
        } else
        {
            destinationReached = false;
        }
    }
}
