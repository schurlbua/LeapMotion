using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton {

	/**************
	 * Attributes *
	***************/

	private static Singleton instance;

	public UIManager UIMgr;

	private List<Gesture> gestureList;

	/***********
	 * Methods *
	************/

	private Singleton() {}

	public static Singleton Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new Singleton();
			}
			return instance;
		}
	}

	public void InitGestureList() {
		gestureList = new List<Gesture> ();

		Gesture rotation = GameObject.FindGameObjectWithTag ("Organ").GetComponent<Rotation> ();
		Gesture translation = GameObject.FindGameObjectWithTag ("Organ").GetComponent<Translation> ();
		gestureList.Add (rotation);
		gestureList.Add (translation);
	}

	public List<Gesture> GetGestureList() {
		return gestureList;
	}
}
