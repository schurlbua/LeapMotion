using System;
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

	protected bool pressed = false;
	protected bool activated = false;
	public bool fading = false;

	protected Vector3 startPosition;

	/***********
	 * Methods *
	************/

	// Use this for initialization
	void Start () {

		// Remember start position of button
		startPosition = transform.localPosition;

		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		float initPos = 0.0f;
		if (!lockX) initPos = startPosition.x;
		else if (!lockY) initPos = startPosition.y;
		else if (!lockZ) initPos = startPosition.z;
		float offset = (activationDistance - initPos) * 0.2f;

		if (transform.localPosition.z > offset)
			moveButton ();
		else
			transform.localPosition = startPosition;
		
		if (activated) {
			activated = false;
			Activation ();
		}
	}

	private void moveButton()
	{
		// Use local position instead of global, so button can be rotated in any direction
		// Lock disabled axis
		Vector3 localPos = transform.localPosition;
		if (lockX) localPos.x = startPosition.x;
		if (lockY) localPos.y = startPosition.y;
		if (lockZ) localPos.z = startPosition.z;
		transform.localPosition = localPos;

		//Get distance of button press. Make sure to only have one moving axis.
		float distance = GetDistance ();

		// Return button to startPosition
		transform.localPosition = Vector3.Lerp (transform.localPosition, startPosition, Time.deltaTime * returnSpeed);

		float pressCompletion = 1.0f - ((activationDistance - distance) / activationDistance);

		// button pressed
		if (pressCompletion >= 0.7f && !pressed)
		{
			pressed = true;
			activated = true;
			//Change color of object to activation color
			StartCoroutine(ChangeButtonColor(gameObject, inactiveColor, activeColor, 0.2f));
		}

		// button unpressed
		else if (pressCompletion <= 0.30f && pressed)
		{
			pressed = false;
			//Change color of object back to normal
			StartCoroutine(ChangeButtonColor(gameObject, activeColor, inactiveColor, 0.3f));
		}
	}

	private IEnumerator ChangeButtonColor(GameObject obj, Color from, Color to, float duration) {
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

	// the button fades away and deactivates itself
	private IEnumerator FadeAway(GameObject obj, float duration) {
		fading = true;

		float timeElapsed = 0.0f;
		float t = 0.0f;

		while (t < 1.0f)
		{
			timeElapsed += Time.deltaTime;
			t = timeElapsed / duration;

			Color colorAlpha = obj.GetComponent<Renderer> ().material.color;
			colorAlpha.a = Mathf.Lerp (1.0f, 0.0f, t);
			obj.GetComponent<Renderer>().material.color = colorAlpha;
			yield return null;
		}

		// For the fade away function, deactivates game object when transparent
		if (gameObject.GetComponent<Renderer> ().material.color.a < 0.05f) {
			fading = false;
			gameObject.SetActive (false);
		}
	}

	// to get the distance between the button and it's start position
	private float GetDistance() {
		Vector3 allDistances = transform.localPosition - startPosition;
		float dist = 1.0f;
		if (!lockX) dist = Math.Abs(allDistances.x);
		else if (!lockY) dist = Math.Abs(allDistances.y);
		else if (!lockZ) dist = Math.Abs(allDistances.z);

		return dist;
	}

	// action of the button when pressed
	void Activation() {
		float r, g, b;
		r = UnityEngine.Random.Range (0.0f, 1.0f);
		g = UnityEngine.Random.Range (0.0f, 1.0f);
		b = UnityEngine.Random.Range (0.0f, 1.0f);

		cube.GetComponent<Renderer> ().material.color = new Color(r, g, b);
	}

	// the button disappears : fades away
	public void Deactivation() {
		if (fading == false) {
			StartCoroutine (FadeAway (gameObject, 0.5f));
		}
	}
}
