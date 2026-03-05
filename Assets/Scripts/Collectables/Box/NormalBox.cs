using UnityEngine;

[CreateAssetMenu(menuName = "Boxes/Normal Box")]
public class NormalBox : BoxBehavior
{
    [Header("Drop")]
    public GameObject fruitPrefab;
    public override void OnHit(Box box, PlayerController player)
    {
        CollectFruit(box);
    }

    public override void OnBreak(Box box)
    {
        base.OnBreak(box);
        if (breakClip != null)
            AudioSource.PlayClipAtPoint(breakClip, box.transform.position);
    }
    
    private void CollectFruit(Box box)
    {
        Transform transform = box.transform;
        GameObject tempFruta = Instantiate(fruitPrefab, transform.position, transform.rotation) as GameObject;
        tempFruta.GetComponent<Animator>().SetTrigger("Coletando");
        tempFruta.GetComponent<AudioSource>().Play();
        GameManager.gm.SetFrutas(1);
        Destroy(tempFruta, 0.667f);
    }
}