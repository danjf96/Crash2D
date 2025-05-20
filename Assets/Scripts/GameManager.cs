using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager gm;
    private int vidas = 2;
    private int frutas = 0;
    // Use this for initialization
    void Awake()
    {
        if (gm == null)
        {
            gm = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        AtualizaHud();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetVidas(int vida)
    {
        vidas += vida;
        if (vidas >= 0)
        {
            AtualizaHud();
        }

    }

    public int GetVidas()
    {
        return vidas;
    }

    public void SetFrutas(int fruta)
    {
        frutas += fruta;
        if (frutas >= 50)
        {
            frutas = 0;
            vidas += 1;
        }

        AtualizaHud();

    }

    public void GetFrutas()
    {

    }

    public void AtualizaHud()
    {

        GameObject.Find("VidasText").GetComponent<Text>().text = vidas.ToString();
        GameObject.Find("FrutaText").GetComponent<Text>().text = frutas.ToString();
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Se for a cena inicial (índice 0), reseta os valores
        if (scene.buildIndex == 0)
        {
            vidas = 2;
            frutas = 0;
        }

        // Atualiza HUD após troca de cena
        AtualizaHud();
    }
    
     void OnDestroy()
    {
        // Evita múltiplos registros se o objeto for recriado por engano
        if (gm == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
