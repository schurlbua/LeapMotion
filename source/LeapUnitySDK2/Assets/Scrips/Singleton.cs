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

	public GameObject Organ;

	public Vector3 initialOrganPosition;
	public Quaternion initialOrganRotation;
	public Vector3 initialOrganLocalScale;

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

	public void Init() {
		InitOrgan ();
		InitGestureList ();
	}

	public void InitOrgan() {
		Organ = GameObject.FindGameObjectWithTag ("Organ");
		initialOrganPosition = Organ.transform.position;
		initialOrganRotation = Organ.transform.rotation;
		initialOrganLocalScale = Organ.transform.localScale;
	}

	public void ResetOrgan (){
		Organ.transform.position = initialOrganPosition;
		Organ.transform.rotation = initialOrganRotation;
		Organ.transform.localScale = initialOrganLocalScale;
	}

	public void InitGestureList() {
		gestureList = new List<Gesture> ();

		Gesture zoom = Organ.GetComponent<Zoom> ();
		Gesture rotation = Organ.GetComponent<Rotation> ();
		Gesture translation = Organ.GetComponent<Translation> ();
		gestureList.Add (zoom);
		gestureList.Add (rotation);
		gestureList.Add (translation);
	}

	public List<Gesture> GetGestureList() {
		return gestureList;
	}
}
