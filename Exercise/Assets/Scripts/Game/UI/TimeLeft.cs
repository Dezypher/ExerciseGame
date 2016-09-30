using UnityEngine;
using System.Collections;

public class TimeLeft : MonoBehaviour {

	private GameLogic gameLogic;
	private UnityEngine.UI.Text text; 

	void Start () {
		gameLogic = GameObject.Find ("GameHandler").GetComponent<GameLogic> ();
		text = GetComponent<UnityEngine.UI.Text> ();
	}

	void Update () {
		int time = Mathf.CeilToInt(gameLogic.currSeconds);

		if (gameLogic.canGetPoint || gameLogic.exerciseType == ExerciseType.Many)
			text.text = time + "s";
		else
			text.text = "";
	}
}
