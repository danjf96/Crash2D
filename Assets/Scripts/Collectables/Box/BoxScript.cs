using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    Animator anim;

    public float jumpForce;
    public GameObject frutaPrefab;
    public AudioClip[] audios;
    private AudioSource audioSrc;
    [SerializeField]
    private float volume;
    public BoxType boxType;

    public Sprite normalSprite;
    public Sprite multipleSprite;
    public Sprite explosiveSprite;
    public Sprite ugaBugaSprite;

    public GameObject ugaBugaPrefab;
    private int hitsToBreak = 1;

    private SpriteRenderer spriteRender;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        audioSrc = gameObject.GetComponent<AudioSource>();
        ConfigureBoxType();
        spriteRender = gameObject.GetComponent<SpriteRenderer>();
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
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        player.OnJump();
        anim.SetTrigger("Colidindo");
        
        if (this.hitsToBreak > 0)
        {
            this.hitsToBreak -= 1;
            switch (boxType)
            {
                case BoxType.Normal:
                    Debug.Log("Normal BOX");
                    CollectFruitPrefab();
                    break;
                case BoxType.UgaBuga:
                    player.OnUgaBugaCollected(ugaBugaPrefab, transform.position);
                    break;
            }   
        }

        if(this.hitsToBreak <= 0)
        {
            DestroyBox();
        }
    }

    public void DestroyBoxByAttack()
    {
        CollectFruitPrefab();
        DestroyBox();
    }   

    private void DestroyBox()
    {
        audioSrc.clip = audios[1];
        AudioSource.PlayClipAtPoint(audios[1], transform.position, volume);
        Destroy(this.gameObject);
    }

    void ConfigureBoxType()
    {
        switch (boxType)
        {
            case BoxType.Normal:
                this.hitsToBreak = 1;
                break;
            case BoxType.UgaBuga:
                this.hitsToBreak = 1;
                // spriteRender.color = Color.red;
                break;
        }
    }  

    void CollectFruitPrefab()
    {
        GameObject tempFruta = Instantiate(frutaPrefab, transform.position, transform.rotation) as GameObject;
        tempFruta.GetComponent<Animator>().SetTrigger("Coletando");
        tempFruta.GetComponent<AudioSource>().Play();
        GameManager.gm.SetFrutas(1);
        Destroy(tempFruta, 0.667f);
    }
 
}
