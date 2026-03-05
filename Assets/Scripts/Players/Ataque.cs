using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque : MonoBehaviour {

    public float forcaHorizontal = 15;
    public float forcaVertical = 10;
    public float tempoDeDestruicao = 1;

    float forcaHorizontalPadrao;
    private Dictionary<string, Action<Collider2D>> actions;
    private void Start()
    {
        forcaHorizontalPadrao = forcaHorizontal;
        actions = new Dictionary<string, Action<Collider2D>>()
        {
            { Tags.Box, OnBoxCollision },
            { Tags.Collectible, OnCollectibleCollision },
            { Tags.Enemy, OnEnemyCollision }
        };
    }
    private void OnBoxCollision(Collider2D boxCollision)
    {
        Box box = boxCollision.GetComponent<Box>();
        box.OnHit(GetComponentInParent<PlayerController>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (actions.TryGetValue(collision.gameObject.tag, out Action<Collider2D> action))
        {
            action.Invoke(collision);
        }
    }

    private void OnCollectibleCollision(Collider2D collision)
    {        
        float direction = (collision.transform.position.x < transform.position.x) ? -1 : 1;

        collision.gameObject.GetComponent<FrutaScript>().DestroyByAttack(tempoDeDestruicao, forcaHorizontal, direction);
    }

    private void OnEnemyCollision(Collider2D collision)
    {
        collision.gameObject.GetComponent<Enemy>().enabled = false;
        BoxCollider2D[] boxes = collision.gameObject.GetComponents<BoxCollider2D>();

        foreach (BoxCollider2D box in boxes)
        {
            box.enabled = false;
        }

        if (collision.transform.position.x < transform.position.x)
            forcaHorizontal *= -1;

        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(forcaHorizontal, forcaVertical), ForceMode2D.Impulse);

        Destroy(collision.gameObject, tempoDeDestruicao);

        forcaHorizontal = forcaHorizontalPadrao;
    }
}
