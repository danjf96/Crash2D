using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour {

    Animator anim;
    private bool vivo = true;

    public AudioClip deathSound;
    private AudioSource audioS;

    
	void Start () {
        anim = gameObject.GetComponent<Animator>();
        audioS = gameObject.GetComponent<AudioSource>();
        GameManager.gm.AtualizaHud();
	}

    private bool Immortal = false; 
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PerdeVida()
    {
        if (vivo && !Immortal)
        {
            audioS.clip = deathSound;
            audioS.Play();
            vivo = false;
            anim.SetTrigger("Morrendo");
            GameManager.gm.SetVidas(-1);
            gameObject.GetComponent<PlayerAttack>().enabled = false;
            gameObject.GetComponent<PlayerController>().enabled = false;
        }
    }

    public void Reset()
    {
        if(GameManager.gm.GetVidas() >= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } else
        {
            SceneManager.LoadScene(4);

        }
    }

    public void SetImmortal(bool value)    {
        Immortal = value;
    }

    public bool GetImmortal()
    {
        return Immortal;
    }  
}
