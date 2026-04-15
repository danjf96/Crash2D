using UnityEngine;

[CreateAssetMenu(menuName = "Boxes/Uga Buga Box")]
public class UgaBugaBox : BoxBehavior
{
    public GameObject ugaBugaPrefab;
    public AudioClip audioUgaBuga;


    public override void OnHit(Box box, PlayerController player)
    {
        var playerManager = player.GetComponent<PlayerManager>();

        if (playerManager != null)
        {
            playerManager.OnUgaBugaCollected(ugaBugaPrefab, box.transform.position);
        }

        if(audioUgaBuga != null)
            AudioSource.PlayClipAtPoint(audioUgaBuga, box.transform.position, 2f);
    }

    public override void OnBreak(Box box)
    {
        base.OnBreak(box);
        if (breakClip != null)
            AudioSource.PlayClipAtPoint(breakClip, box.transform.position);
    }
}