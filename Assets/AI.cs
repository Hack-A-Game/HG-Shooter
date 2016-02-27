using UnityEngine;
using System.Collections;

public class AI : Soldier
{
    Vector3 _lookDirection = Vector3.zero;

    // Use this for initialization
    public override void Start () {
        base.Start();

        _handleInput = false;
	}
	
	// Update is called once per frame
	override public void Update () {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -_lookDirection, 4f);

#if UNITY_EDITOR
        Debug.DrawLine(transform.position, -_lookDirection * 4f, Color.red);
#endif

        if (hit.transform && hit.transform.name == "Soldier-Player")
        {
            _animator.SetInteger("State", FIRE_ANIMATION);
            fire();
        }
        else
        {
            _animator.SetInteger("State", WALK_ANIMATION);
        }

        base.Update();
    }

    public void LookAt(Vector3 position)
    {
        _lookDirection = transform.position - position;
        float rotation = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * 180.0f / Mathf.PI + 90.0f;        
        if (rotation <= 65 || (rotation >= -65 && rotation < 0) || rotation >= 295)
        {
            transform.rotation = Quaternion.Euler(0, 0, rotation);
        }
    }
}
