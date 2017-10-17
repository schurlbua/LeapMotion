using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap;

public class Hand_collision : MonoBehaviour {
	Controller controller;
	public HandController handCtrl;
	public bool trigger;


	// Use this for initialization
	void Start () 
	{
		controller = new Controller ();
		Frame frame = controller.Frame (); 
		HandList handsInFrame = frame.Hands;


	}


	private bool IsHand(Collider other)
	{
		
		if (other.transform.parent && other.transform.parent.parent && other.transform.parent.parent.GetComponent<HandModel> ())
			return true;
		else
			return false;
	}

	void OnTriggerEnter(Collider other) 
	{
		
		if (IsHand (other)) 
		{
			trigger = true;
		}   
	}  

	void OnTriggerExit(Collider other)
	{
		if (IsHand (other)) 
		{
			trigger = false;
		}
	}

	void Update(){
		Frame frame = controller.Frame (); 
		Hand hand = frame.Hands.Frontmost;

		if(trigger==true)
		transform.rotation = handCtrl.transform.rotation * hand.Basis.Rotation (false); //rotation
	}

}

