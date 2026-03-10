using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed;
    public float jumpForce = 700;

    Rigidbody2D rb;
    bool facingRight = false;
    bool noChao = false;
    Transform groundCheck;

    private AudioSource audioS;


	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        audioS = gameObject.GetComponent<AudioSource>();
        groundCheck = transform.Find("EnemyGroundCheck");
	}
	
	// Update is called once per frame
	void Update () {

        noChao = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (!noChao)
            speed *= -1;
	}

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);

        if(speed > 0 && !facingRight)
        {
            Flip();
        }
        else if(speed < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioS.Play();
            BoxCollider2D[] boxes = gameObject.GetComponents<BoxCollider2D>();

            foreach (BoxCollider2D box in boxes)
            {
                box.enabled = false;
            }

            collision.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));

            speed = 0;
            transform.Rotate(new Vector3(0, 0, -180));
            Destroy(gameObject, 3);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.GetComponent<PlayerLife>().GetImmortal())
            {
                // Ataque atk = collision.gameObject.GetComponentInParent<Ataque>();
                OnColliderByAtaque(15f, 1f);
                return;
            }

            collision.gameObject.GetComponent<PlayerLife>().PerdeVida();
        }
    }

    public void OnColliderByAtaque(float forcaHorizontal, float tempoDeDestruicao)
    {
        Enemy enemy = GetComponent<Enemy>();

        enemy.enabled = false;
        BoxCollider2D[] boxes = enemy.GetComponents<BoxCollider2D>();

        foreach (BoxCollider2D box in boxes)
        {
            box.enabled = false;
        }

        if (enemy.transform.position.x < transform.position.x)
            forcaHorizontal *= -1;

        enemy.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(forcaHorizontal, 0), ForceMode2D.Impulse);

        Destroy(enemy.gameObject, tempoDeDestruicao);
    }
}
