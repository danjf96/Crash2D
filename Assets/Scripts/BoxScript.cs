using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    Animator anim;

    public float jumpForce;
    public int frutas;
    public GameObject frutaPrefab;
    public AudioClip[] audios;
    private AudioSource audioSrc;

    [SerializeField]
    private float volume;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        audioSrc = gameObject.GetComponent<AudioSource>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            Boolean isFloating = player.IsFloating();
            if(!isFloating)
            {
                return;
            }

            JumpingTheBox(collision);
               
        }
    }

    void JumpingTheBox(Collider2D collision)
    {
        audioSrc.clip = audios[0];
        audioSrc.Play();
        collision.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
        collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        anim.SetTrigger("Colidindo");
        if (frutas > 0)
        {
            GameObject tempFruta = Instantiate(frutaPrefab, transform.position, transform.rotation) as GameObject;
            tempFruta.GetComponent<Animator>().SetTrigger("Coletando");
            tempFruta.GetComponent<AudioSource>().Play();
            frutas -= 1;
            GameManager.gm.SetFrutas(1);
            Destroy(tempFruta, 0.667f);
        }
        else
        {
            DestroyBox();
        }
    }

    public void DestroyBoxByAttack()
    {
        GameObject tempFruta = Instantiate(frutaPrefab, transform.position, transform.rotation) as GameObject;
        tempFruta.GetComponent<Animator>().SetTrigger("Coletando");
        tempFruta.GetComponent<AudioSource>().Play();
        GameManager.gm.SetFrutas(1);
        DestroyBox();
    }   

    private void DestroyBox()
    {
        audioSrc.clip = audios[1];
        AudioSource.PlayClipAtPoint(audios[1], transform.position, volume);
        Destroy(this.gameObject);
    }
}
