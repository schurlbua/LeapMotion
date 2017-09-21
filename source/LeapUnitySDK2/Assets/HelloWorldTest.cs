using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap;

public class HelloWorldTest : MonoBehaviour {
	Controller controller;


	// Use this for initialization
	void Start () {
		controller = new Controller ();

		GetComponent<Renderer>().material.color = Color.yellow;
		
	}
	
	// Update is called once per frame
	void Update () {
		Frame frame = controller.Frame();
		HandList handsInFrame = frame.Hands;
		Bone bone = Bone.Invalid;
		bool trigger_pinch = false;

		FingerList allFingers = frame.Fingers;
		Pointable pointable = frame.Pointables.Frontmost;
		Finger finger = new Finger (pointable);
		String fingerDesc = finger.ToString ();
		String boneDesc = bone.ToString();
		float THUMB_TRIGGER_DISTANCE = 0.05f;

	
		Hand firstHand = handsInFrame [0];
		Vector3 thumb_tip = firstHand.Fingers [0].TipPosition.ToUnityScaled ();




		for (int i = 1; i < firstHand.Fingers.Count && !trigger_pinch; ++i) {
			Finger finger2 = firstHand.Fingers[i];

			for (int j = 0; j < 22 && !trigger_pinch; ++j) {
				Vector3 joint_position = finger2.JointPosition((Finger.FingerJoint)(j)).ToUnityScaled();
				Vector3 distance = thumb_tip - joint_position;
				if (distance.magnitude < THUMB_TRIGGER_DISTANCE)
					trigger_pinch = true;
					//GetComponent<Renderer> ().material.color = Color.black;
					
			}
		}

		if (handsInFrame.Count > 0) {
			GetComponent<Renderer> ().material.color = Color.green;
			if (finger != Finger.Invalid) {
				//Debug.Log (finger.Type);
				//Debug.Log (bone.Type);
				Debug.Log (trigger_pinch);
				Debug.Log (thumb_tip);
			}
		} else {
			GetComponent<Renderer> ().material.color = Color.red;
		}

	}

	/*void UpdatePinch(Frame frame) {
		bool trigger_pinch = false;
		Hand hand = frame.Hands[handIndex];

		// Thumb tip is the pinch position.
		Vector3 thumb_tip = hand.Fingers[0].TipPosition.ToUnityScaled();

		// Check thumb tip distance to joints on all other fingers.
		// If it's close enough, start pinching.
		for (int i = 1; i < NUM_FINGERS && !trigger_pinch; ++i) {
			Finger finger = hand.Fingers[i];

			for (int j = 0; j < NUM_JOINTS && !trigger_pinch; ++j) {
				Vector3 joint_position = finger.JointPosition((Finger.FingerJoint)(j)).ToUnityScaled();
				Vector3 distance = thumb_tip - joint_position;
				if (distance.magnitude < THUMB_TRIGGER_DISTANCE)
					trigger_pinch = true;
			}
		}

		// Only change state if it's different.
		if (trigger_pinch && !pinching_)
			OnPinch(pinch_position);
	}*/

}
