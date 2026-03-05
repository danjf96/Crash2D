using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private BoxBehavior behavior;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource audioSrc;
    private int currentHits;

    private void Awake()
    {
        currentHits = behavior.hitsToBreak;
        if (behavior.animatorController != null)
            anim.runtimeAnimatorController = behavior.animatorController;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        PlayerController player = collision.GetComponent<PlayerController>();
        if (!player.IsFloating()) return;

        OnHit(player);
    }

    public void OnHit(PlayerController player)
    {
        if (!string.IsNullOrEmpty(behavior.hitTrigger))
            anim.SetTrigger(behavior.hitTrigger);

        if (behavior.hitClip != null)
            audioSrc.PlayOneShot(behavior.hitClip);

        currentHits--;
       
        if(player.transform.position.y > transform.position.y)
        {
            player.Bounce();
        }
       

        player.OnJump();

        behavior.OnHit(this, player);

        if (currentHits <= 0)
        {
            Break();
        }
    }

    private void Break()
    {
       behavior.OnBreak(this);
    }
}