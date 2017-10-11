using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap;

public class UITest : MonoBehaviour {

	Controller controller;

	public Button testButton;

	// Use this for initialization
	void Start () {
		controller = new Controller ();

		Button btn = testButton.GetComponent<Button> ();
		btn.onClick.AddListener (ChangeColor);

		GetComponent<Renderer>().material.color = Color.blue;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ChangeColor() {
		float r, g, b;
		r = Random.Range (0.0f, 1.0f);
		g = Random.Range (0.0f, 1.0f);
		b = Random.Range (0.0f, 1.0f);

		Debug.Log (r);
		Debug.Log (g);
		Debug.Log (b);

		GetComponent<Renderer> ().material.color = new Color(r, g, b);
		//GetComponent<Renderer> ().material.color = new Color(0.4f, 0.2f, 0.7f);
	}

	void ChangeColorOnHands() {
		Frame frame = controller.Frame();
		HandList handsInFrame = frame.Hands;
		if (handsInFrame.Count > 0) {
			GetComponent<Renderer> ().material.color = Color.yellow;
		} else {
			GetComponent<Renderer> ().material.color = Color.red;
		}
	}
}
