using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;


public class Rotation : Gesture {

	Controller controller;
	public HandController handCtrl;


	// Use this for initialization
	void Start () 
	{
		controller = new Controller ();
	}

	private bool IsHand(Collider other)
	{
		if (other.transform.parent && other.transform.parent.parent && other.transform.parent.parent.GetComponent<HandModel> ())
			return true;
		else
			return false;
	}

	void OnTriggerStay(Collider other) 
	{
		if (IsHand (other)) 
		{
			Frame frame = controller.Frame (); 
			Hand hand = frame.Hands.Frontmost;

			transform.rotation = handCtrl.transform.rotation * hand.Basis.Rotation (false); //rotation
			Activated = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		Activated = false;
	}
}
