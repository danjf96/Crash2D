using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class PlayerAttack : MonoBehaviour {

    Animator anim;
    public float intevaloDeAtaque;
    private float nextAttack;

    public AudioClip spinSound;
    private AudioSource audioS;
    private PlayerInputActions input;
    Boolean isAttacking = false;

	void Start () {
        anim = gameObject.GetComponent<Animator>();
        audioS = gameObject.GetComponent<AudioSource>();
	}
	
    void Awake()
    {
        input = new PlayerInputActions();

        input.Player.Attack.performed += ctx => Attack();
    }

    void OnEnable() => input.Enable();
    void OnDisable() => input.Disable();

    void Attack()
    {
        if (Time.time > nextAttack)
        {
            isAttacking = true;
            audioS.clip = spinSound;
            audioS.Play();
            anim.SetTrigger("Ataque");
            nextAttack = Time.time + intevaloDeAtaque;
        }
    }

    public void OnAttackFinished()
    {
       isAttacking = false;
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }
}
