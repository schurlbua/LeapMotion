﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap;

public class ButtonManager : MonoBehaviour {


	/**************
	 * Attributes *
	***************/

	public GameObject cube;

	public bool lockX;
	public bool lockY;
	public bool lockZ;

	public float returnSpeed;
	public float activationDistance;

	public Color inactiveColor;
	public Color activeColor;

	protected Controller controller;

	protected bool pressed = false;
	protected bool released = false;
	protected bool activated = false;

	protected Vector3 startPosition;


	/***********
	 * Methods *
	************/

	// Use this for initialization
	void Start () {
		controller = new Controller ();

		// Remember start position of button
		startPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		Frame frame = controller.Frame();
		HandList handsInFrame = frame.Hands;

		released = false;

		// Use local position instead of global, so button can be rotated in any direction
		Vector3 localPos = transform.localPosition;
		if (lockX) localPos.x = startPosition.x;
		if (lockY) localPos.y = startPosition.y;
		if (lockZ) localPos.z = startPosition.z;
		transform.localPosition = localPos;

		// Return button to startPosition
		transform.localPosition = Vector3.Lerp(transform.localPosition, startPosition, Time.deltaTime * returnSpeed);

		//Get distance of button press. Make sure to only have one moving axis.
		Vector3 allDistances = transform.localPosition - startPosition;
		float distance = 1.0f;
		if (!lockX) distance = Math.Abs(allDistances.x);
		else if (!lockY) distance = Math.Abs(allDistances.y);
		else if (!lockZ) distance = Math.Abs(allDistances.z);
		float pressCompletion = Mathf.Clamp(1 / activationDistance * distance, 0f, 1f);

		//Activate pressed button
		if (pressCompletion >= 0.7f && !pressed)
		{
			pressed = true;
			//Change color of object to activation color
			StartCoroutine(ChangeButtonColor(gameObject, inactiveColor, activeColor, 0.2f));
		}

		//Dectivate unpressed button
		else if (pressCompletion <= 0.2f && pressed)
		{
			pressed = false;
			released = true;
			activated = true;
			//Change color of object back to normal
			StartCoroutine(ChangeButtonColor(gameObject, activeColor, inactiveColor, 0.3f));
		}

		if (activated) {
			activated = false;
			Activation ();
		}
	}

	private IEnumerator ChangeButtonColor(GameObject obj, Color from, Color to, float duration)
	{
		float timeElapsed = 0.0f;
		float t = 0.0f;

		while (t < 1.0f)
		{
			timeElapsed += Time.deltaTime;
			t = timeElapsed / duration;
			obj.GetComponent<Renderer>().material.color = Color.Lerp(from, to, t);
			yield return null;
		}

	}

	void Activation() {
		float r, g, b;
		r = UnityEngine.Random.Range (0.0f, 1.0f);
		g = UnityEngine.Random.Range (0.0f, 1.0f);
		b = UnityEngine.Random.Range (0.0f, 1.0f);

		cube.GetComponent<Renderer> ().material.color = new Color(r, g, b);
	}
}
