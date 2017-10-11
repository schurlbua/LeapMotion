using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTest : MonoBehaviour {

	protected Vector3 target;
	protected Vector3 start;

	// Use this for initialization
	void Start () {
		start = transform.localPosition;
		target = transform.localPosition;
		target.x = 8;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = Vector3.Lerp (transform.localPosition, target, Time.deltaTime * 0.5f);
	}
}
