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
	void Update () {

		Frame frame = controller.Frame ();
		HandList handsInFrame = frame.Hands;
		Hand hand = frame.Hands.Frontmost;
		bool trigger_pinch = false;
		bool trigger_pinch2 = false;
		bool zooming = false;
		Pointable pointable = frame.Pointables.Frontmost;
		Finger finger = new Finger (pointable);
		float THUMB_TRIGGER_DISTANCE = 0.03f;
		Hand firstHand = handsInFrame [0];
		Hand secondHand = handsInFrame [1];
		Vector3 thumb_tip = firstHand.Fingers [0].TipPosition.ToUnityScaled ();
		Vector3 thumb_tip2 = secondHand.Fingers [0].TipPosition.ToUnityScaled ();

		for (int i = 1; i < firstHand.Fingers.Count && !trigger_pinch; ++i) {
			Finger finger2 = firstHand.Fingers [i];

			for (int j = 0; j < 22 && !trigger_pinch; ++j) {
				Vector3 joint_position = finger2.JointPosition ((Finger.FingerJoint)(j)).ToUnityScaled ();
				Vector3 distance = thumb_tip - joint_position;
				if (distance.magnitude < THUMB_TRIGGER_DISTANCE)
				trigger_pinch = true;
			}
		}

		for (int i = 1; i < secondHand.Fingers.Count && !trigger_pinch2; ++i) {
			Finger finger3 = secondHand.Fingers [i];

			for (int j = 0; j < 22 && !trigger_pinch2; ++j) {
				Vector3 joint_position2 = finger3.JointPosition ((Finger.FingerJoint)(j)).ToUnityScaled ();
				Vector3 distance2 = thumb_tip2 - joint_position2;
				if (distance2.magnitude < THUMB_TRIGGER_DISTANCE)
				trigger_pinch2 = true;
			}
		}

		/*if (handsInFrame.Count == 2 && trigger_inside && trigger_pinch && trigger_pinch2) {
		Debug.Log ("beide");
		//transform.localScale += new Vector3 (0.1F, 0.1F, 0.1F);
		}*/

		if (handsInFrame.Count > 0) {
			GetComponent<Renderer> ().material.color = Color.green;
			if (finger != Finger.Invalid) {
				if (handsInFrame.Count == 2 && trigger_inside && trigger_pinch && trigger_pinch2) {
					Vector3 distance3 = thumb_tip - thumb_tip2;
					float zoomdistance = 0;
					for(int i = 0; i<3; i++){
						if (distance3[i]>0 && distance3[i]>zoomdistance){
							zoomdistance = distance3[i];
						}
					}
					//Debug.Log(zoomdistance);
					zooming = true;
					//transform.localScale += distance3;
				} else if ((trigger_pinch || trigger_pinch2) && trigger_inside && !zooming) {
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

	void OnTriggerEnter (Collider other)
	{
		//Debug.Log (other.gameObject.name);
		trigger_inside = true;
	}
}
