using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Bullet : MonoBehaviour
{
    private const float SPEED = 500.0f;
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
        if (other.gameObject.name != "EnemyDetector")
        {
            if (other.gameObject.name == "Soldier-Player")
            {
                Text life = GameObject.Find("Life").GetComponent<Text>();
                Int32 newLife = (Int32.Parse(life.text) - 10);
                life.text = newLife.ToString();

                if (newLife == 0)
                {
                    Application.LoadLevel("GameOver");
                }
            }
            Destroy(gameObject);
        }
    }
}
