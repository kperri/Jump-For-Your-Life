using UnityEngine;
using System.Collections;

public class FallingFire : MonoBehaviour {

	Rigidbody2D fire;
	float speed;
	bool startGame;

	// Use this for initialization
	void Start () {
		FindObjectOfType<CountdownScript> ().countdownEvent += OnCountdownFinish;
		FindObjectOfType<CountdownScript> ().pauseEvent += FreezeFire;
		speed = 5.0f;
		startGame = true;
		fire = GetComponent<Rigidbody2D> ();
	}

	void OnCountdownFinish () {
		startGame = true;
	}

	void FreezeFire () {
		startGame = false;
	}

	void FixedUpdate () {
		if (startGame) {
			Vector2 grow = -fire.transform.up * speed * Time.deltaTime;
			fire.MovePosition (fire.position + grow);
		}
	}
}
