using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;


public class UIManager {

	public HandController handController;

	protected Controller controller;

	protected List<GameObject> buttonList;

	// Use this for initialization
	public UIManager () {
		handController = GameObject.FindObjectOfType<HandController>();
		controller = new Controller ();

		buttonList = new List<GameObject> ();

		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Button")) {
			buttonList.Add (go);
		}
	}

	// Update is called once per frame
	public void Update () {
		/*Frame frame = controller.Frame();
		HandList handsInFrame = frame.Hands;

		foreach (Hand hand in handsInFrame) {
			Vector3 handPos = handController.transform.TransformPoint(hand.Fingers[2].TipPosition.ToUnityScaled());
			if (handPos.z < 0) {
				foreach (GameObject button in buttonList) {
					if (button.GetComponent<Collider> ().bounds.Contains (handPos)) {
						button.transform.Find ("MovingButton").gameObject.SetActive (true);
					} else if (button.transform.Find ("MovingButton").gameObject.activeInHierarchy) {
						button.transform.Find ("MovingButton").gameObject.GetComponent<ButtonManager> ().Deactivation ();
					}
				}
			}
		}*/
	}
}
