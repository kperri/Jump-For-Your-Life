using UnityEngine;
using System.Collections;

public class FireStarter : MonoBehaviour {

	public Rigidbody2D fire;

	Transform match;
	float fireVel;

	// Use this for initialization
	void Start () {
		match = GetComponentInChildren<Transform> ();
		fireVel = 30f;
	}
	
	// Update is called once per frame
	void Update () {
		InvokeRepeating ("SpawnFire", 2, 5);
	}

	void SpawnFire () {
		Rigidbody2D fireInstance = 
			Instantiate (fire, match.position, match.rotation) as Rigidbody2D;

		fireInstance.velocity = fireVel * match.forward;
	}
}
