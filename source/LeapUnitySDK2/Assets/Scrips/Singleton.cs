using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour {

	/**************
	 * Attributes *
	***************/

	private static Singleton instance;

	public UIManager UIMgr;

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
}
