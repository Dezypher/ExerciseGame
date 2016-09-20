using UnityEngine;
using System.Collections;

public class ColorIndicator : MonoBehaviour {

	public Color colorActive;
	public Color colorInactive;

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
			image.color = colorActive;
		} else {
			image.color = colorInactive;
		}
	}
}
