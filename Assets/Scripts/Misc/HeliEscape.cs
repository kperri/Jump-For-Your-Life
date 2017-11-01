using UnityEngine;
using System.Collections;

public class HeliEscape : MonoBehaviour {

	public bool dropOff;
	
	Rigidbody2D helicopter;
	bool fly;
	Vector2 flyUp;

	// Use this for initialization
	void Start () {
		helicopter = GetComponent<Rigidbody2D> ();
		fly = false;
		dropOff = true;
	}

	void FixedUpdate () {
		if (fly) {
			helicopter.MovePosition (helicopter.position + flyUp);
		}
	}

	public void Fly () {
		flyUp = helicopter.transform.up * 5 * Time.deltaTime;
		fly = true;
		dropOff = false;
		Invoke ("DropOff", 5.0f);
	}

	void DropOff () {
		fly = false;
		gameObject.SetActive (false);
		dropOff = true;
	}
}
