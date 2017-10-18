using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonVisibilityManager : MonoBehaviour {

	protected GameObject movingButton;

	// Use this for initialization
	void Start () {
		movingButton = transform.Find ("MovingButton").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider otherCollider) {
		if (otherCollider.transform.parent.name == "middle" && otherCollider.gameObject.name == "bone1") {
			movingButton.SetActive (true);
		}
	}

	void OnTriggerExit(Collider otherCollider) {
		if (otherCollider.transform.parent.name == "middle" && otherCollider.gameObject.name == "bone1" && movingButton.activeInHierarchy) {
			Debug.Log ("deactivation");
			movingButton.GetComponent<ButtonManager> ().Deactivation ();
		}
	}
}
