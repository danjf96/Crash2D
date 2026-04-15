using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int lifes;
    public int fruits;
    public Vector3 checkpoint;

    public bool hasMask;
    public int maskHits;
}
