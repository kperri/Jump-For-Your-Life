using UnityEngine;
using System;
using System.Collections;

public class DeleteFire : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.gameObject.CompareTag ("Balloon")) {
			gameObject.SetActive (false);
		}
	}
}
