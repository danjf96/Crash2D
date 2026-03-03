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
        col.transform.position += new Vector3(forceHorizontal * direction * 0.1f, 0, 0);
        Destroy(gameObject, timeOfDestroy);
    }
}
