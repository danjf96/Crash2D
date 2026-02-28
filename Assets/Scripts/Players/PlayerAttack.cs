using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    Animator anim;
    public float intevaloDeAtaque;
    private float nextAttack;

    public AudioClip spinSound;
    private AudioSource audioS;
    private PlayerInputActions input;

	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
        audioS = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
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
            audioS.clip = spinSound;
            audioS.Play();
            anim.SetTrigger("Ataque");
            nextAttack = Time.time + intevaloDeAtaque;
        }
    }
}
