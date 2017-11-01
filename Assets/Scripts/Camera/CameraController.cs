using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    GameObject player;
	Vector3 cameraPos;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		cameraPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
	}

	void LateUpdate () {
		cameraPos.y = player.transform.position.y + 3f;
		transform.position = cameraPos;
	}
}
