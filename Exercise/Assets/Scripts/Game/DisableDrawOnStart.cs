using UnityEngine;
using System.Collections;

public class DisableDrawOnStart : MonoBehaviour {

	void Start () {
		GetComponent<MeshRenderer>().enabled = false;
	}
}
