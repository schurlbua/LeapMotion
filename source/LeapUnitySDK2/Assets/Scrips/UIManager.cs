using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap;


public class UIManager : MonoBehaviour {

	public UnityEngine.UI.Image gestureImage;

	// Use this for initialization
	void Start () {

		// Init for Gesture feedback icon
		gestureImage = GameObject.Find("GestureIcon").GetComponent<UnityEngine.UI.Image>();
		gestureImage.enabled = false;
	}

	public void DetectGesture() {
		bool imageEnabled = false;
		foreach (Gesture gesture in Singleton.Instance.GetGestureList()) {
			if (gesture.Activated) {
				gestureImage.enabled = true;
				gestureImage.sprite = gesture.Icon;
				imageEnabled = true;
				break;
			}
		}
		if (imageEnabled == false) {
			gestureImage.enabled = false;
		}
	}

	void Update () {
		DetectGesture ();
	}
}
