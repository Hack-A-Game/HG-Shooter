using UnityEngine;
using System.Collections;

public class EnemyDetector : MonoBehaviour {

    AI _ai;

	// Use this for initialization
	void Start () {
        _ai = transform.parent.GetComponent<AI>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Bullet")
        {
            _ai.LookAt(other.transform.position);
            Debug.Log(other.name);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag != "Bullet")
        {
            _ai.LookAt(other.transform.position);
        }
    }
}
