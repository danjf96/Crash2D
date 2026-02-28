using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour {

    Animator anim;
    private bool vivo = true;

    public AudioClip deathSound;
    private AudioSource audioS;
	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
        audioS = gameObject.GetComponent<AudioSource>();
        GameManager.gm.AtualizaHud();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PerdeVida()
    {
        if (vivo)
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
}
