using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;


public class UIManager : MonoBehaviour {

	protected Controller controller;

	protected List<GameObject> buttonList;

	// Use this for initialization
	void Start () {
		controller = new Controller ();


	}
	
	// Update is called once per frame
	void Update () {
		Frame frame = controller.Frame();
		HandList handsInFrame = frame.Hands;

		foreach (Hand hand in handsInFrame) {
			Vector3 pos = UnityVectorExtension.ToUnityScaled(hand.PalmPosition);
			Debug.Log (pos);
		}
	}
}
