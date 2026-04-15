using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private GameObject currentUgaBuga;

    private PlayerData data;

    public GameObject ugaBugaPrefab;

    public static PlayerManager Instance;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        data = GameManager.gm.playerData;

        if (data.hasMask)
        {
            SpawnMask();
        }
    }

    public void SpawnMask()
    {
        if (currentUgaBuga == null)
        {
            currentUgaBuga = Instantiate(ugaBugaPrefab, transform);
        }
    }

    public void RemoveMask()
    {
        if (currentUgaBuga != null)
        {
            Destroy(currentUgaBuga);
        }
    }

     public void OnUgaBugaCollected(GameObject ugaBuga, Vector3 position)
    {
        if (currentUgaBuga == null)
        {
            GameObject ugaBugaObj = Instantiate(ugaBugaPrefab, position, Quaternion.identity);
            currentUgaBuga = ugaBugaObj;
        }

        if(currentUgaBuga != null)
        {
            
            if (currentUgaBuga.TryGetComponent<UgaBuga>(out var script))
            {
                script.UpdatedCollectedCount();
            }
        }

    }
}
