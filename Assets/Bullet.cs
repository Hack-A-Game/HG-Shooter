using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    private const float SPEED = 200.0f;
    private Rigidbody2D _rigidBody;

	void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
	
	void Update()
    {
        Vector2 velocity = new Vector2(0, SPEED * Time.deltaTime);
        _rigidBody.velocity = transform.rotation * velocity;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
