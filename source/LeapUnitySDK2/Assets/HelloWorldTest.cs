using System;
using UnityEngine;
using Leap;

public class HelloWorldTest : MonoBehaviour
{
	Controller controller;
	bool trigger_inside = false;
	public HandController handCtrl;

	void Start ()	{
		controller = new Controller ();
		GetComponent<Renderer> ().material.color = Color.yellow;
	}

	void Update () {
		Frame frame = controller.Frame ();
		HandList handsInFrame = frame.Hands;
		Hand hand = frame.Hands.Frontmost;
		bool trigger_pinch1 = false;
		bool trigger_pinch2 = false;
		bool zooming = false;
		Finger finger = new Finger (frame.Pointables.Frontmost);
		float THUMB_TRIGGER_DISTANCE = 0.03f;
		Hand firstHand = handsInFrame [0];
		Hand secondHand = handsInFrame [1];
		Vector3 thumb_tip1 = firstHand.Fingers [0].TipPosition.ToUnityScaled ();
		Vector3 thumb_tip2 = secondHand.Fingers [0].TipPosition.ToUnityScaled ();

		// checking if hands pinch

		for (int i = 1; i < firstHand.Fingers.Count && !trigger_pinch1; ++i) {
			Finger finger2 = firstHand.Fingers [i];

			for (int j = 0; j < 22 && !trigger_pinch1; ++j) {
				Vector3 joint_position = finger2.JointPosition ((Finger.FingerJoint)(j)).ToUnityScaled ();
				Vector3 distance = thumb_tip1 - joint_position;
				if (distance.magnitude < THUMB_TRIGGER_DISTANCE)
				trigger_pinch1 = true;
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

		// executing the movement

		if (handsInFrame.Count > 0) {
			GetComponent<Renderer> ().material.color = Color.green;
			if (finger != Finger.Invalid) {
				//zoomfunction - not working yet
				if (handsInFrame.Count == 2 && trigger_inside && trigger_pinch1 && trigger_pinch2) {
					Vector3 distance3 = thumb_tip1 - thumb_tip2;
					float zoomdistance = 0;
					for(int i = 0; i<3; i++){
						if (distance3[i]>0 && distance3[i]>zoomdistance){
							zoomdistance = distance3[i];
						}
					}
					//Debug.Log(zoomdistance);
					zooming = true;
					//transform.localScale += distance3;
				}
				// translation function -  if hand is inside and pinched -> you can move the object
				else if ((trigger_pinch1 || trigger_pinch2) && trigger_inside && !zooming) {
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

	// checking if hands are colliding with the object

	void OnTriggerEnter (Collider other)
	{
		trigger_inside = true;
	}
}
