using UnityEngine;
using System.Collections;

public class Recycler : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.gameObject.CompareTag("Player")) {
			gameObject.SetActive (false);
		}
	}
}
