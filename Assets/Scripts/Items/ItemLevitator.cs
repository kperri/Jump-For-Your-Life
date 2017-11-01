using UnityEngine;
using System.Collections;

public class ItemLevitator : MonoBehaviour {

    Rigidbody2D levitatingObject;
    bool up;

    void Start () {
        levitatingObject = gameObject.GetComponent<Rigidbody2D> ();
        InvokeRepeating ("UpAndDown", 1.5f, 1.5f);
    }

    void FixedUpdate () {
        Vector2 levitate = levitatingObject.transform.up * Time.deltaTime;
        Vector2 fall = -levitatingObject.transform.up * Time.deltaTime;
        if (up) {
            gameObject.GetComponent<Rigidbody2D> ().MovePosition (levitatingObject.position + levitate);
        } else if (!up) {
            gameObject.GetComponent<Rigidbody2D> ().MovePosition (levitatingObject.position + fall);
        }
    }

    void UpAndDown () {
        up = !up;
    }
}
