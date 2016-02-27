using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 lookDirection = transform.position - other.transform.position;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(lookDirection.y, lookDirection.x) * 180.0f / Mathf.PI + 90.0f);
    }
}
