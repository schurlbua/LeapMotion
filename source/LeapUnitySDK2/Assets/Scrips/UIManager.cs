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

				// making sure translating icon gets selected if translating and rotating at the same time.
				if(gesture.GetType().Equals(typeof(Rotation))){
					foreach (Gesture otherGesture in Singleton.Instance.GetGestureList()) {
						if(otherGesture.GetType().Equals(typeof(Translation))&& otherGesture.Activated){
							gestureImage.sprite = otherGesture.Icon;
							break;
						}
					}
				}

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
