using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour {
	static bool isActive = false;
	void Awake () {
		if (!isActive)
		{
			DontDestroyOnLoad(this.gameObject);
			isActive = true;
		}
		else{
			Destroy(this.gameObject);
		}
	}
}
