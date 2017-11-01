using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BalloonThrow : MonoBehaviour {

	public Text balloonCount;
	public GameObject balloon;
	public GameObject player;
	public event Action balloonEvent;


	int balloons;
	bool startGame;
	Vector2 leftThrow;
	Vector2 rightThrow;
	Rigidbody2D theBalloon;
	GameObject newBalloon;

	// Use this for initialization
	void Start () {
		FindObjectOfType<CharacterMovement> ().deathEvent += FreezeSpawner;
		FindObjectOfType<CharacterMovement> ().balloonEvent += BalloonCounter;
		FindObjectOfType<CountdownScript> ().countdownEvent += OnCountdownFinish;
		FindObjectOfType<CountdownScript> ().pauseEvent += FreezeSpawner;
		leftThrow = new Vector2(-20, 5);
		rightThrow = new Vector2(20, 5);
		balloons = 3;
		balloonCount.text = "Water Balloons: " + balloons.ToString ();
		startGame = false;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			Throw ();
		}
	}

	public void Throw () {
		if (balloons > 0 && startGame && Time.timeScale > 0) {
			newBalloon = (GameObject)Instantiate (balloon, player.transform.position, Quaternion.Euler (0,0,0));
			balloon.GetComponent<SpriteRenderer> ().flipX = player.GetComponent<SpriteRenderer> ().flipX;
			theBalloon = newBalloon.GetComponent<Rigidbody2D> ();
			if (player.GetComponent<SpriteRenderer> ().flipX == false) {
				theBalloon.AddForce (leftThrow, ForceMode2D.Impulse);
			} else if (player.GetComponent<SpriteRenderer> ().flipX == true) {
				theBalloon.AddForce(rightThrow, ForceMode2D.Impulse);
			}
			balloons--;
			balloonCount.text = "Water Balloons: " + balloons.ToString ();
		}
		if (balloonEvent != null) {
			balloonEvent ();
		}
	}

	void BalloonCounter () {
		balloons++;
		balloonCount.text = "Water Balloons: " + balloons.ToString ();
	}

	void OnCountdownFinish () {
		startGame = true;
	}

	void FreezeSpawner () {
		startGame = false;
	}
}
