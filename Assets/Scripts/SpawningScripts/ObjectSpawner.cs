using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour {
	
	public GameObject fallingObject;

	Transform theSpawner;

	// Use this for initialization
	void Start () {
		theSpawner = gameObject.GetComponent<Transform> ();
		FindObjectOfType<CountdownScript> ().countdownEvent += OnCountdownFinish;
		FindObjectOfType<CountdownScript> ().pauseEvent += FreezeSpawner;
		PoolManager.instance.CreateNewPool (fallingObject, 2);
	}

	void Spawn () {
		PoolManager.instance.ReuseObject (fallingObject, theSpawner.position, theSpawner.rotation);
	}

	void OnCountdownFinish () {
		InvokeRepeating ("Spawn", 10f, 20f);
	}

	void FreezeSpawner () {
		CancelInvoke ();
	}
}
