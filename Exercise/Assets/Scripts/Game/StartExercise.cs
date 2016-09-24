using UnityEngine;
using System.Collections;

public class StartExercise : MonoBehaviour {

	private GameLogic gameLogic;

	void Awake() {
		gameLogic = GameObject.Find ("GameHandler").GetComponent<GameLogic> ();
	}

	// Use this for initialization
	public void OnClick() {
		gameLogic.StartStage ();
	}
}