using UnityEngine;
using System.Collections;

public class SpawnerMover : MonoBehaviour {

	Transform player;
	Vector3 temp;
	float randomNumber;
	//private Rigidbody2D spawner;
	bool startGame;
//	private bool tooFarLeft;
//	private bool tooFarDown;

	// Use this for initialization
	void Start () {
		player = GetComponentInParent<Transform> ();
		temp = transform.position;
		Random.InitState ((int)System.DateTime.Now.Ticks);
		InvokeRepeating ("GenerateRandomNumber", 5, 5);
		//spawner = GetComponent<Rigidbody2D> ();
//		tooFarLeft = true;
//		tooFarDown = true;
	}

	// Update is called once per frame
	void Update () {
		if (randomNumber >= 0.5f) {
			temp.x = 2f;
			temp.y = player.transform.position.y;
			temp.z = -1f;
			transform.position = temp;
		} else if (randomNumber < 0.5f) {
			temp.x = -2f;
			temp.y = player.transform.position.y;
			temp.z = -1f;
			transform.position = temp;
		}
//		if (spawner.transform.position.x > 2) {
//			tooFarLeft = false;
//		} else if (transform.position.x < -2) {
//			tooFarLeft = true;
//		}
//		if (spawner.transform.position.y > 35) {
//			tooFarDown = false;
//		} else if (spawner.transform.position.y < 30) {
//			tooFarDown = true;
//		}
//		if (tooFarLeft && tooFarDown) {
//			Vector2 slide = spawner.transform.up * 1 * Time.deltaTime;
//			Vector2 fly = -spawner.transform.right * 1 * Time.deltaTime;
//			spawner.MovePosition (spawner.position + slide + fly);
//		} else if (!tooFarLeft && !tooFarDown) {
//			Vector2 slide = -spawner.transform.up * 1 * Time.deltaTime;
//			Vector2 fly = spawner.transform.right * 1 * Time.deltaTime;
//			spawner.MovePosition (spawner.position + slide + fly);
//		} else if (!tooFarLeft && tooFarDown) {
//			Vector2 slide = -spawner.transform.up * 1 * Time.deltaTime;
//			Vector2 fly = -spawner.transform.right * 1 * Time.deltaTime;
//			spawner.MovePosition (spawner.position + slide + fly);
//		} else if (tooFarLeft && !tooFarDown) {
//			Vector2 slide = spawner.transform.up * 1 * Time.deltaTime;
//			Vector2 fly = spawner.transform.right * 1 * Time.deltaTime;
//			spawner.MovePosition (spawner.position + slide + fly);
//		}
	}

	void GenerateRandomNumber () {
		randomNumber = Random.Range (0f, 1f);
	}
}
