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
		if (handsInFrame.Count > 0) {
			GetComponent<Renderer> ().material.color = Color.green;
		} else {
			GetComponent<Renderer> ().material.color = Color.red;
		}

	}
}
