using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class Translation : Gesture {

	Controller controller;
	bool trigger_inside = false;
	public bool trigger_insideL = false;
	public bool trigger_insideR = false;
	public HandController handCtrl;
	public float oldZoomdistance;
	public bool isLeftFirst = false;
	public bool isLeftSecond = false;
	public bool zooming = false;
	public bool translating = false;
	public bool trigger_pinch1 = false;
	public bool trigger_pinch2 = false;
	public Vector3 thumb_tip1;
	public Vector3 thumb_tip2;


	void Start ()	{
		controller = new Controller ();
	}

	void Update () {
		Frame frame = controller.Frame ();
		HandList handsInFrame = frame.Hands;
		Hand hand = frame.Hands.Frontmost;
		trigger_pinch1 = false;
		trigger_pinch2 = false;
		Finger finger = new Finger (frame.Pointables.Frontmost);
		float THUMB_TRIGGER_DISTANCE = 0.025f;
		Hand firstHand = handsInFrame [0];
		Hand secondHand = handsInFrame [1];
		thumb_tip1 = firstHand.Fingers [0].TipPosition.ToUnityScaled ();
		thumb_tip2 = secondHand.Fingers [0].TipPosition.ToUnityScaled ();
		isLeftFirst = firstHand.IsLeft ? true : false;
		isLeftSecond = secondHand.IsLeft ? true : false;

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
			if (finger != Finger.Invalid) {
				// translation function -  if hand is inside and pinched -> you can move the object
				if ((trigger_pinch1 || trigger_pinch2) && trigger_inside && !zooming) {
					Activated = true;
					transform.position = handCtrl.transform.TransformPoint (hand.Fingers [0].TipPosition.ToUnityScaled ()); //translation
				} else {
					trigger_inside = false;
					trigger_insideL = false;
					trigger_insideR = false;
				}
			}
		}
	}

	// checking if hands are colliding with the object

	void OnTriggerStay (Collider other) {

		if(other.transform.parent.root.name == "RigidRoundHandLeft(Clone)"){
			trigger_insideL = true;
			} else if (other.transform.parent.root.name == "RigidRoundHandRight(Clone)"){
				trigger_insideR = true;
			}
			trigger_inside = true;
		}

		void OnTriggerExit (Collider other) {
			trigger_insideL = false;
			trigger_insideR = false;
			Activated = false;
			translating = false;
			zooming = false;
		}
	}
