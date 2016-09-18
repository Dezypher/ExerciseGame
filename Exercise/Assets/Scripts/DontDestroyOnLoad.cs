using UnityEngine;
using System.Collections;

public class DontDestroyOnLoad : MonoBehaviour {

	private bool instantiated;

	void Awake() {
		if (!instantiated) {
			DontDestroyOnLoad (this.gameObject);
			instantiated = true;
		}
	}

}
