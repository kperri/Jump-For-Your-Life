using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MovingFire : MonoBehaviour {

	public Vector2 speedMaxMin;
	public float speedIncreaser;

	GameObject player;
	Rigidbody2D fire;
	bool startGame;
	float speed;
	float maxDistance;

	// Use this for initialization
	void Start () {
		FindObjectOfType<CharacterMovement> ().deathEvent += FreezeFire;
		FindObjectOfType<CountdownScript> ().countdownEvent += OnCountdownFinish;
		FindObjectOfType<CountdownScript> ().pauseEvent += FreezeFire;
		player = GameObject.Find ("Character");
		fire = GetComponent<Rigidbody2D> ();
		startGame = false;
		speed = speedMaxMin.y;
		maxDistance = 10;
	}

	void Update () {
		if (speedMaxMin.y < speedMaxMin.x && startGame) {
			speedMaxMin.y += speedIncreaser * Time.deltaTime;
		}
		if (speedMaxMin.y > speedMaxMin.x) {
			speedMaxMin.y = speedMaxMin.x;
		}
		if (player.transform.position.y - transform.position.y >= maxDistance) {
			speed = 50;
		} else if (player.transform.position.y - transform.position.y < maxDistance/2) {
			speed = speedMaxMin.y;
		}
	}

	void OnCountdownFinish () {
		startGame = true;
	}

	void FreezeFire () {
		startGame = false;
	}

	void FixedUpdate () {
		if (startGame) {
			Vector2 grow = fire.transform.right * speed * Time.deltaTime;
			fire.MovePosition (fire.position + grow);
		}
	}
}
