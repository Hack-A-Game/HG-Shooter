using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Bullet : MonoBehaviour
{
    private const float SPEED = 500.0f;
    private Rigidbody2D _rigidBody;
    private GameObject _owner;

	void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
	
	void Update()
    {
        Vector2 velocity = new Vector2(0, SPEED * Time.deltaTime);
        _rigidBody.velocity = transform.rotation * velocity;
	}

    public void setOwner(GameObject owner)
    {
        _owner = owner;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name != "EnemyDetector")
        {
            if (other.gameObject == _owner)
            {
                return;
            }

            if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
            {
                float hp = other.gameObject.GetComponent<Soldier>().takeBullet(this);

                if (other.gameObject.tag == "Player")
                {
                    Text life = GameObject.Find("Life").GetComponent<Text>();
                    life.text = hp.ToString();

                    if (hp == 0)
                    {
                        Application.LoadLevel("GameOver");
                    }
                }
            }

            Destroy(gameObject);
        }
    }
}
