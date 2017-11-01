using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : ButtonManager {

	// Virtualisation of the function Activation : Reset function is activated
	public override void Activation () {
		organ.transform.position = initialOrganPosition;
		organ.transform.rotation = initialOrganRotation;
		organ.transform.localScale = initialOrganLocalScale;
	}
}
