using UnityEngine;

public abstract class BoxBehavior : ScriptableObject
{
    [Header("Config")]
    public int hitsToBreak = 1;
    public float bounceForce = 12f;

    [Header("Audio")]
    public AudioClip hitClip;
    public AudioClip breakClip;

    [Header("Animation")]
    public RuntimeAnimatorController animatorController;
    public string hitTrigger = "Hit";


    public abstract void OnHit(Box box, PlayerController player);

    public virtual void OnBreak(Box box)
    {
        Destroy(box.gameObject);
    }
}