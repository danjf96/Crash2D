using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    Animator anim;
    public float intevaloDeAtaque;
    private float proximoAtaque;

    public AudioClip spinSound;
    private AudioSource audioS;

	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
        audioS = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetButtonDown("Fire1") && Time.time > proximoAtaque)
        {
            Atacando();
        }
		
	}

    void Atacando()
    {
        audioS.clip = spinSound;
        audioS.Play();
        anim.SetTrigger("Ataque");
        proximoAtaque = Time.time + intevaloDeAtaque;
    }
}
