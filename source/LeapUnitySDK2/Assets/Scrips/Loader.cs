using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Singleton.Instance.UIMgr = new UIManager ();
	}

}
