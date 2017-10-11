using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LMWidgets;


public class ToggleButtonDataBinder : DataBinderToggle {

	bool toggle = false;

	override public bool GetCurrentData() {
		return toggle;
	}

	override protected void setDataModel(bool value) {
		toggle = value;
	}

	void ChangeColor() {
		float r, g, b;
		r = Random.Range (0.0f, 1.0f);
		g = Random.Range (0.0f, 1.0f);
		b = Random.Range (0.0f, 1.0f);

		GetComponent<Renderer> ().material.color = new Color(r, g, b);
	}


	void Update() {
		if (toggle) {
			ChangeColor ();
		}
	}
}
