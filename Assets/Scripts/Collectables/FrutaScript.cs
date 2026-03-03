using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrutaScript : MonoBehaviour
{
    Animator anim;
    CircleCollider2D col;
    private AudioSource audioS;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        col = gameObject.GetComponent<CircleCollider2D>();
        audioS = gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerAttack playerAttack = collision.gameObject.GetComponent<PlayerAttack>();
            if(playerAttack.IsAttacking())
            {
                return;
            }
            audioS.Play();
            GameManager.gm.SetFrutas(1);
            col.enabled = false;
            anim.SetTrigger("Coletando");
            Destroy(gameObject, 0.667f);

        }
    }

    public void DestroyByAttack(float timeOfDestroy, float forceHorizontal, float direction)
    {
        col.enabled = false;
        this.enabled = false;

        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.bodyType = RigidbodyType2D.Dynamic;

        Vector2 dir = new Vector2(direction, 1f).normalized;
        rb.AddForce(dir * forceHorizontal, ForceMode2D.Impulse);
                
        Destroy(gameObject, timeOfDestroy);
    }
}
