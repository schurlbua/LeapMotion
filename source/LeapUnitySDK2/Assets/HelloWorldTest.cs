using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap;

public class HelloWorldTest : MonoBehaviour
{
	Controller controller;
	bool trigger_inside = false;
	public HandController handCtrl;

	// Use this for initialization
	void Start ()
	{
		controller = new Controller ();
		GetComponent<Renderer> ().material.color = Color.yellow;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Frame frame = controller.Frame ();
		HandList handsInFrame = frame.Hands;
		Hand hand = frame.Hands.Frontmost;
		Bone bone = Bone.Invalid;
		bool trigger_pinch = false;
		FingerList allFingers = frame.Fingers;
		Pointable pointable = frame.Pointables.Frontmost;
		Finger finger = new Finger (pointable);
		float THUMB_TRIGGER_DISTANCE = 0.03f;
		Hand firstHand = handsInFrame [0];
		Vector3 thumb_tip = firstHand.Fingers [0].TipPosition.ToUnityScaled ();

		for (int i = 1; i < firstHand.Fingers.Count && !trigger_pinch; ++i) {
			Finger finger2 = firstHand.Fingers [i];

			for (int j = 0; j < 22 && !trigger_pinch; ++j) {
				Vector3 joint_position = finger2.JointPosition ((Finger.FingerJoint)(j)).ToUnityScaled ();
				Vector3 distance = thumb_tip - joint_position;
				if (distance.magnitude < THUMB_TRIGGER_DISTANCE)
					trigger_pinch = true;
			}
		}

		if (handsInFrame.Count > 0) {
			GetComponent<Renderer> ().material.color = Color.green;
			if (finger != Finger.Invalid) {
				if (trigger_pinch && trigger_inside) {
					GetComponent<Renderer> ().material.color = Color.blue;
					transform.position = handCtrl.transform.TransformPoint (hand.Fingers [0].TipPosition.ToUnityScaled ()); //translation
				} else {
					trigger_inside = false;
				}
			}
		} else {
			GetComponent<Renderer> ().material.color = Color.red;
		}

	}

	void OnTriggerEnter (Collider other) {
		Debug.Log (other.gameObject.name);
		trigger_inside = true;
	}
}