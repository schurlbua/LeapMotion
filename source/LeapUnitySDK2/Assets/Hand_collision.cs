using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap;

public class Hand_collision : MonoBehaviour {
	Controller controller;
	public HandController handCtrl;


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
		Frame frame = controller.Frame (); 
		Hand hand = frame.Hands.Frontmost;
		if (IsHand (other)) 
		{
			Debug.Log ("Yay! A hand collided!");
			//GetComponent<Renderer> ().material.color = Color.yellow;
			//transform.position = handCtrl.transform.TransformPoint(hand.PalmPosition.ToUnityScaled()); //translation
			transform.rotation = handCtrl.transform.rotation * hand.Basis.Rotation (false); //rotation

			/*Renderer[] renders = GetComponentsInChildren<Renderer> ();
			foreach (Renderer render in renders) 
			{
				Color new_color = render.material.color;
				new_color.g = 255;
				render.material.color = new_color;
			}*/
				
		} 


		    
	}  


	/*
	// Update is called once per frame
	void Update (Collider other) {
		bool colide = false;
		Hand leap_hand = GetComponent<HandModel>().GetLeapHand();
		float confidence = leap_hand.Confidence;

		if (other.transform.parent && other.transform.parent.parent && other.transform.parent.parent.GetComponent<HandModel> ()) {
			colide = true;
		} 
		else 
		{
			colide = false;
		}
		if (colide == true) 
		{
			Renderer[] renders = GetComponentsInChildren<Renderer> ();
			foreach (Renderer render in renders) 
			{
				Color new_color = render.material.color;
				new_color.a = 0;
				new_color.r = 153;
				render.material.color = new_color;
			}
		}
	}


	

	protected void SetRendererAlpha(Renderer render, float alpha) 
	{
		Color new_color = render.material.color;
		new_color.a = 0;
		render.material.color = new_color;
	}*/
}

