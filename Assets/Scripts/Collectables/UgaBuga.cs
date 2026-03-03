using UnityEngine;

public class UgaBuga: MonoBehaviour
{        
    private Transform player;
    public float speed = 8f;
    public float minDistance = 1.5f;
    public Vector2 offset = new Vector2(0f, 1.5f);

    private int count = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    void Update()
    {
        if (player == null) return;

        Vector2 targetPosition = (Vector2)player.position + offset;

        float distance = Vector2.Distance(transform.position, targetPosition);

        if (distance > minDistance)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPosition,
                speed * Time.deltaTime
            );
        }
    }

    public void UpdatedCollectedCount()
    {
        if(count  >= 1) return;

        count += 1;
    }

    public int GetCountCollected()
    {
       return count;
    }
}
