using UnityEngine;

public class CoconutEncounter : MonoBehaviour
{
    [SerializeField] private AudioClip coconutBonk;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Bot"))
        {
            collision.GetComponent<Stunnable>().ApplyStun();

            if(collision.CompareTag("Player"))
            {
                SFXManager.instance.PlaySFX(coconutBonk);
            }

            Destroy(gameObject);
        }
    }
}
