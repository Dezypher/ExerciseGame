using UnityEngine;
using System.Collections;

public class ColorIndicator : MonoBehaviour {

	private GameLogic gameLogic;
	private UnityEngine.UI.Image image;

	void Awake() {
		gameLogic = GameObject.Find ("GameHandler").GetComponent<GameLogic> ();
		image = GetComponent<UnityEngine.UI.Image> ();
	}

	// Update is called once per frame
	void Update () {
		Color color = new Color ();

		if (gameLogic.isDoingExercise) {
			color.r = 0.1f;
			color.g = 0.6f;
			color.b = 0.1f;
			color.a = 1f;
		} else {
			color.r = 0.6f;
			color.g = 0.1f;
			color.b = 0.1f;
			color.a = 1f;
		}

		image.color = color;
	}
}
