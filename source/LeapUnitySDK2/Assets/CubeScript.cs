using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap;

public class CubeScript : MonoBehaviour {

	Controller controller;
	public GameObject Cube;

	// Use this for initialization
	void Start () {
		controller = new Controller ();

		controller.EnableGesture(Gesture.GestureType.TYPESWIPE); //Enable Gesture Swipe
		controller.Config.SetFloat("Gesture.Swipe.MinLength",10.0f); //Set minimum Length swipe so it gets registered
		controller.Config.SetFloat("Gesture.Swipe.MinVelocity",10.0f); //Set minimum Velocity swipe so it gets registered
		controller.Config.Save(); //saves the configs


		controller.EnableGesture(Gesture.GestureType.TYPECIRCLE); //Enable Gesture Swipe
		controller.Config.SetFloat("Gesture.Swipe.MinLength",10.0f); //Set minimum Length swipe so it gets registered
		controller.Config.SetFloat("Gesture.Swipe.MinVelocity",10.0f); //Set minimum Velocity swipe so it gets registered
		controller.Config.Save(); //saves the configs


		controller.EnableGesture (Gesture.GestureType.TYPESCREENTAP); //Enable Gesture Swipe
		controller.Config.SetFloat ("Gesture.KeyTap.MinDownVelocity", 40.0f);
		controller.Config.SetFloat ("Gesture.KeyTap.HistorySeconds", .2f);
		controller.Config.SetFloat ("Gesture.KeyTap.MinDistance", 1.0f);
		controller.Config.Save ();//saves the configs


		controller.EnableGesture (Gesture.GestureType.TYPEKEYTAP); //Enable Gesture Swipe
		controller.Config.SetFloat ("Gesture.KeyTap.MinDownVelocity", 40.0f);
		controller.Config.SetFloat ("Gesture.KeyTap.HistorySeconds", .2f);
		controller.Config.SetFloat ("Gesture.KeyTap.MinDistance", 1.0f);
		controller.Config.Save ();//saves the configs


	}
	
	// Update is called once per frame
	void Update () {

		Frame frame = controller.Frame();
		HandList handsInFrame = frame.Hands;
		GestureList gestures = frame.Gestures ();
		Hand hand = frame.Hands.Frontmost;




		/*
		//CIRCLE
		if (hand.IsLeft) 

		{
			GetComponent<Renderer>().material.color = Color.red;

			//gesture.count is the number of gestures that have been done --> if you do a gesture --> gesture.count = gesture.count + 1
			//i is 0 at the beginning and gesture.count too --> if you do gesture --> gesture.count = 1 --> loop starts --> at the end i = i++ = 1
			//so gesture.count not higher than i --> no new loop
			for (int i = 0; i < gestures.Count; i++) 
			{ 



				Gesture gesture = gestures[i];

				if (gesture.Type == CircleGesture.ClassType ()) {
					CircleGesture circleGesture = new CircleGesture (gesture);





					//F---P
					//Vector swipeDirection = Swipe.Direction; //forcevector that will rotate the cube
					//Vector swipeDirection = Swipe.Direction;


					string clockwiseness;
					if (circleGesture.Pointable.Direction.AngleTo (circleGesture.Normal) <= Math.PI / 2) {
						clockwiseness = "clockwise";
					} else {
						clockwiseness = "counterclockwise";
					}


					if (clockwiseness == "clockwise") 
					{
						Debug.Log ("Left"); //Left appears in the console
						//spinLeft = true;
						//spinRight = false;
						Cube.transform.Rotate (Vector3.left, 45 * Time.deltaTime);

					}
					if (clockwiseness == "counterclockwise") 
					{
						Debug.Log ("Right"); //Right appears in the console
						//spinRight = true;
						//spinLeft = false;
						Cube.transform.Rotate (Vector3.right, 45 * Time.deltaTime);

					}

				}
			}

		}


		*/



		//SCREENTAP
		if (hand.IsLeft) 

		{
			GetComponent<Renderer>().material.color = Color.red;

			//gesture.count is the number of gestures that have been done --> if you do a gesture --> gesture.count = gesture.count + 1
			//i is 0 at the beginning and gesture.count too --> if you do gesture --> gesture.count = 1 --> loop starts --> at the end i = i++ = 1
			//so gesture.count not higher than i --> no new loop
			for (int i = 0; i < gestures.Count; i++) 
			{ 
				Gesture gesture = gestures[i];

				if (gesture.Type == Gesture.GestureType.TYPE_SCREEN_TAP) {


					ScreenTapGesture screentapGesture = new ScreenTapGesture (gesture);

					Vector screenT = screentapGesture.Direction;

					Debug.Log (screenT);


					if (screenT.y <= 100 && screenT.y >= -100) 					

					{
						Debug.Log ("tapping"); //Left appears in the console
						//spinLeft = true;
						//spinRight = false;
						//Cube.transform.localPosition =  Vector3.left * 5;

						Cube.transform.localScale += new Vector3(1,1,1)*2;
					}

				}
			}

		}

		//SCREENTAP
		if (hand.IsRight) 

		{
			GetComponent<Renderer>().material.color = Color.yellow;

			//gesture.count is the number of gestures that have been done --> if you do a gesture --> gesture.count = gesture.count + 1
			//i is 0 at the beginning and gesture.count too --> if you do gesture --> gesture.count = 1 --> loop starts --> at the end i = i++ = 1
			//so gesture.count not higher than i --> no new loop
			for (int i = 0; i < gestures.Count; i++) 
			{ 
				Gesture gesture = gestures[i];

				if (gesture.Type == Gesture.GestureType.TYPE_SCREEN_TAP) {


					ScreenTapGesture screentapGesture = new ScreenTapGesture (gesture);

					Vector screenT = screentapGesture.Direction;

					Debug.Log (screenT);


					if (screenT.y <= 100 && screenT.y >= -100) 					

					{
						Debug.Log ("tapping"); //Left appears in the console
						//spinLeft = true;
						//spinRight = false;
						//Cube.transform.localPosition =  Vector3.left * 5;

						Cube.transform.localScale -= new Vector3(1,1,1)/1.5f;
					}

				}
			}

		}


		//KEYTAP
		/*
		if (hand.IsLeft) 

		{
			GetComponent<Renderer>().material.color = Color.red;

			//gesture.count is the number of gestures that have been done --> if you do a gesture --> gesture.count = gesture.count + 1
			//i is 0 at the beginning and gesture.count too --> if you do gesture --> gesture.count = 1 --> loop starts --> at the end i = i++ = 1
			//so gesture.count not higher than i --> no new loop
			for (int i = 0; i < gestures.Count; i++) 
			{ 



				Gesture gesture = gestures[i];

				if(gesture.Type == KeyTapGesture.ClassType()) {


					KeyTapGesture keytapGesture = new KeyTapGesture(gesture);
				
					Vector keyD = keytapGesture.Direction;


					Debug.Log (keyD);




					if (keyD.y <= 100 && keyD.y >= -100) 
					{
						Debug.Log ("WORKING"); //Left appears in the console
						//spinLeft = true;
						//spinRight = false;
						Cube.transform.Rotate (transform.rotation.x, transform.rotation.y, transform.rotation.z + 170);

					}



				}

			}

		}



		*/


		// SWIPE
		if (hand.IsRight) 

		{
			GetComponent<Renderer>().material.color = Color.yellow;

			//gesture.count is the number of gestures that have been done --> if you do a gesture --> gesture.count = gesture.count + 1
			//i is 0 at the beginning and gesture.count too --> if you do gesture --> gesture.count = 1 --> loop starts --> at the end i = i++ = 1
			//so gesture.count not higher than i --> no new loop
			for (int i = 0; i < gestures.Count; i++) 
			{ 
				Gesture gesture = gestures[i];
				if (gesture.Type == Gesture.GestureType.TYPESWIPE) //when swipe recognized
				{
					SwipeGesture Swipe = new SwipeGesture (gesture);
					//F---P
					//Vector swipeDirection = Swipe.Direction; //forcevector that will rotate the cube
					Vector swipeDirection = Swipe.Direction;



					if (swipeDirection.x < 0) 
					{
						
						Cube.transform.Rotate (Vector3.up, 70 * Time.deltaTime);

		
					}
					if (swipeDirection.x > 0) 
					{	
						
						Cube.transform.Rotate (Vector3.down, 70 * Time.deltaTime);
			

					}

				}
			}

		}


		// SWIPE
		if (hand.IsLeft) 

		{
			GetComponent<Renderer>().material.color = Color.blue;

			//gesture.count is the number of gestures that have been done --> if you do a gesture --> gesture.count = gesture.count + 1
			//i is 0 at the beginning and gesture.count too --> if you do gesture --> gesture.count = 1 --> loop starts --> at the end i = i++ = 1
			//so gesture.count not higher than i --> no new loop
			for (int i = 0; i < gestures.Count; i++) 
			{ 
				Gesture gesture = gestures[i];
				if (gesture.Type == Gesture.GestureType.TYPESWIPE) //when swipe recognized
				{
					SwipeGesture Swipe = new SwipeGesture (gesture);
					//F---P
					//Vector swipeDirection = Swipe.Direction; //forcevector that will rotate the cube
					Vector swipeDirection = Swipe.Direction;



					if (swipeDirection.x < 0) 
					{

						Cube.transform.Rotate (Vector3.up, 70 * Time.deltaTime);


					}
					if (swipeDirection.x > 0) 
					{	

						Cube.transform.Rotate (Vector3.down, 70 * Time.deltaTime);


					}

				}
			}

		}


	}
}
