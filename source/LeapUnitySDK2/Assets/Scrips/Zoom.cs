using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class Zoom : Gesture {
	Controller controller;
	private Translation T;

	void Start () {
		controller = new Controller ();
		T = gameObject.GetComponent<Translation>();
	}

	void Update () {
		Frame frame = controller.Frame ();
		HandList handsInFrame = frame.Hands;
		bool trigger_insideR = T.trigger_insideR;
		bool trigger_insideL = T.trigger_insideL;
		bool trigger_pinch1 = T.trigger_pinch1;
		bool trigger_pinch2 = T.trigger_pinch2;
		bool zooming = T.zooming;
		float oldZoomdistance = T.oldZoomdistance;
		Vector3 thumb_tip1 = T.thumb_tip1;
		Vector3 thumb_tip2 = T.thumb_tip2;

		if (handsInFrame.Count == 2 && trigger_insideR && trigger_insideL && trigger_pinch1 && trigger_pinch2) {
			float distance3 = Vector3.Distance(thumb_tip1,thumb_tip2)*50;
			if(oldZoomdistance == distance3) {
				zooming = false;
			} else {
				zooming = true;
				Activated = true;
				transform.localScale = Vector3.one * distance3;
			}
			oldZoomdistance = distance3;
		} else {
			Activated = false;
		}
	}
}
