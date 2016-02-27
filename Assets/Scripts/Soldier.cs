using UnityEngine;
using System.Collections;

public class Soldier : MonoBehaviour
{
    Transform _wrapperTransform;
    Animator _animator;
    Rigidbody2D _rigiBody;

    private const float SPEED = 80.0f;
    private const int WALK_ANIMATION = 0;
    private const int FIRE_ANIMATION = 1;

    private const float BULLETS_PER_SECOND = 0.5f;
    private float _lastBullet;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigiBody = transform.parent.gameObject.GetComponent<Rigidbody2D>();
        _lastBullet = Time.time;
    }
	
	void Update()
    {
	    if (Input.GetMouseButton(0))
        {
            _animator.SetInteger("State", FIRE_ANIMATION);

            if (Time.time - _lastBullet > 1.0 / BULLETS_PER_SECOND)
            {
                _lastBullet = Time.time;

                GameObject gob = AssetLoader.get().instantiate("Bullet");
                gob.transform.position = transform.position;
                gob.transform.rotation = transform.rotation;
            }
        }
        else
        {
            _animator.SetInteger("State", WALK_ANIMATION);
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 lookDirection = transform.position - mousePosition;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(lookDirection.y, lookDirection.x) * 180.0f / Mathf.PI + 90.0f);

        Vector2 velocity = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            velocity += new Vector2(0, SPEED * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity += new Vector2(0, -SPEED * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity += new Vector2(-SPEED * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity += new Vector2(SPEED * Time.deltaTime, 0);
        }
        velocity = transform.rotation * velocity;
        _rigiBody.velocity = velocity;
    }
}
