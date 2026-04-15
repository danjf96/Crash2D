using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager gm;
    public PlayerData playerData;
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

    public void SetLife(int life)
    {
        playerData.lifes += life;
        if (life >= 0)
        {
            AtualizaHud();
        }

    }

    public int GetVidas()
    {
        return playerData.lifes;
    }

    public void SetFruits(int fruit)
    {
        playerData.fruits += fruit;
        if (playerData.fruits >= 50)
        {
            playerData.fruits = 0;
            playerData.lifes += 1;
        }

        AtualizaHud();

    }

    public int GetFruits()
    {
        return playerData.fruits;
    }

    public void AtualizaHud()
    {

        GameObject.Find("VidasText").GetComponent<Text>().text = playerData.lifes.ToString();
        GameObject.Find("FrutaText").GetComponent<Text>().text = playerData.fruits.ToString();
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Se for a cena inicial (índice 0), reseta os valores
        if (scene.buildIndex == 0)
        {
            playerData.lifes = 2;
            playerData.fruits = 0;
        }

        // Atualiza HUD após troca de cena
        AtualizaHud();
    }
    
    void OnDestroy()
    {
        // Evita múltiplos registros se o objeto for recriado por engano
        if (gm == this)
        {
            ResetPlayerData();
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    void ResetPlayerData()
    {
        playerData.lifes = 3;
        playerData.fruits = 0;
        SceneManager.LoadScene(0);
    }
}
