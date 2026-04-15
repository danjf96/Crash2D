using UnityEngine;
using System.Threading.Tasks;

public class UgaBuga: MonoBehaviour
{        
    private GameObject player;
    public float speed = 8f;
    public float minDistance = 1.5f;
    public Vector2 offset = new(0f, 1.5f);

    private int count = 0;

    void Start()
    {
        count = 1;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player == null) return;

        Vector2 targetPosition = (Vector2)player.transform.position + offset;

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
        if(count == 3)
        {
            SetImmortalTemporarily();
            return;
        }

        count += 1;
    }

    public int GetCountCollected()
    {
       return count;
    }

    public void LostUgaBuga()
    {
        if(count <= 0)
        {
            return;
        }

        count -= 1;
    }

    private async void SetImmortalTemporarily()
    {
        PlayerLife playerLife = player.GetComponent<PlayerLife>();
        playerLife.SetImmortal(true);

        SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();
        Color originalColor = playerSprite.color;

        playerSprite.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);

        await Task.Delay(5000);
        LostUgaBuga();
        playerSprite.color = originalColor;
        playerLife.SetImmortal(false);
        
    }
}
