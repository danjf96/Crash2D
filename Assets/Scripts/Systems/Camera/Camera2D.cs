using UnityEngine;
using System.Collections;

public class Camera2D : MonoBehaviour
{

	private Vector2 velocity;

	public float smoothTimeY;
	public float smoothTimeX;
	public float deltaY;

	public GameObject player;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		smoothTimeY = 0.2f;
		smoothTimeX = 0.2f;
		deltaY = 0.4f;
	}

	void FixedUpdate()
	{

		float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y + deltaY, ref velocity.y, smoothTimeY);

		transform.position = new Vector3(posX, posY, transform.position.z);
	}
}