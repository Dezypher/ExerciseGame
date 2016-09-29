using UnityEngine;
using System.Collections;

public class DegradeAlpha : MonoBehaviour {

	public float rate;

	private UnityEngine.UI.Image image;

	void Start() {
		image = GetComponent<UnityEngine.UI.Image> ();
	}

	// Update is called once per frame
	void Update () {
		Color color = image.color;

		if (image.color.a > 0) {
			color.a -= Time.deltaTime * rate;
		} else {
			color.a = 0;
		}

		image.color = color;
	}
}
