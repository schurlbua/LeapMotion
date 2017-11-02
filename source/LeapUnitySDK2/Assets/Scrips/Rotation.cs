using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;


public class Rotation : Gesture {

	Controller controller;
	public HandController handCtrl;

	private Quaternion oldHandRotation;

	// Use this for initialization
	void Start () {
		controller = new Controller ();
		oldHandRotation = Quaternion.identity;
	}

	private bool IsHand(Collider other) {
		if (other.transform.parent && other.transform.parent.parent && other.transform.parent.parent.GetComponent<HandModel> ())
			return true;
		else
			return false;
	}

	void OnTriggerEnter() {
		oldHandRotation = Quaternion.identity;
	}

	void OnTriggerStay(Collider other) {
		if (IsHand (other)) 
		{
			Frame frame = controller.Frame (); 
			Hand hand = frame.Hands.Frontmost;

			Quaternion newHandRotation = handCtrl.transform.rotation * hand.Basis.Rotation (false);

			if (oldHandRotation != Quaternion.identity) {
				Quaternion deltaRotation = newHandRotation * Quaternion.Inverse (oldHandRotation);
				transform.rotation = deltaRotation * transform.rotation;

				//transform.rotation = handCtrl.transform.rotation * hand.Basis.Rotation (false); //rotation
				Activated = true;
			}

			oldHandRotation = handCtrl.transform.rotation * hand.Basis.Rotation (false);
		}
	}

	void OnTriggerExit() {
		Activated = false;
		oldHandRotation = Quaternion.identity;
	}
}
