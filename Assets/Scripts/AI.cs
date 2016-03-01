using UnityEngine;
using System.Collections;

public class AI : Soldier
{
    private Vector3 _lookDirection = Vector3.zero;
    private bool lookingAtEnemy = false;

    // Use this for initialization
    public override void Start () {
        base.Start();

        _handleInput = false;
	}

    override public float takeBullet(Bullet bullet)
    {
        LookAt(bullet.transform.position, true);

        return base.takeBullet(bullet);
    }


    // Update is called once per frame
    override public void Update()
    {
        _rigidBody.velocity = Vector2.zero;

        if (lookingAtEnemy)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _lookDirection, 3.5f, Constants.LAYER_PLAYER_MASK | Constants.LAYER_WALL_MASK);

#if UNITY_EDITOR
            Debug.DrawLine(transform.position, transform.position + _lookDirection * 3.5f, Color.red);
#endif

            if (!hit.transform)
            {
                hit = Physics2D.Raycast(transform.position, _lookDirection, 5f, Constants.LAYER_PLAYER_MASK | Constants.LAYER_WALL_MASK);
                if (hit.transform && hit.transform.tag == "Player")
                {
                    _rigidBody.velocity = transform.rotation * (new Vector2(0, SPEED) * Time.deltaTime);
                }
            }

            if (hit.transform && hit.transform.tag == "Player")
            {
                _animator.SetInteger("State", FIRE_ANIMATION);
                fire();
            }
            else
            {
                _animator.SetInteger("State", WALK_ANIMATION);
                lookingAtEnemy = false;
            }
        }

        base.Update();
    }

    public void LookAt(Vector3 position, bool forceRotate = false)
    {
        Vector3 lookDirection = (transform.position - position).normalized;
        float rotation = Mathf.Atan2(lookDirection.y, lookDirection.x) * 180.0f / Mathf.PI + 90.0f;
        float realtiveRotation = Mathf.Abs(transform.rotation.eulerAngles.z - rotation);

        if (forceRotate || (realtiveRotation <= 65 || realtiveRotation >= 295))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (position - transform.position).normalized, 4f, Constants.LAYER_PLAYER_MASK | Constants.LAYER_WALL_MASK);

#if UNITY_EDITOR
            Debug.DrawLine(transform.position, transform.position + (position - transform.position).normalized * 4, Color.green);
#endif
            
            if (hit.transform && hit.transform.tag == "Player")
            {
                transform.rotation = Quaternion.Euler(0, 0, rotation);
                _lookDirection = (position - transform.position).normalized;
                lookingAtEnemy = true;
            }
        }
    }
}
