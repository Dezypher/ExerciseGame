using UnityEngine;
using System.Collections;

public class Prompt : MonoBehaviour {

	public GameLogic gameLogic;
	public UnityEngine.UI.Text text; 

	void Start () {
		text = GetComponent<UnityEngine.UI.Text> ();
	}

	void Update () {
		int time = Mathf.CeilToInt(gameLogic.currSeconds);

		if (!gameLogic.done) {
			if (gameLogic.started) {
				if (!gameLogic.resting && gameLogic.canGetPoint) {
					text.text = "GO!";
				} else if (gameLogic.success) {
					text.text = "GOOD JOB!";
				} else if (gameLogic.failed) {
					text.text = "TOO BAD!";
				}

				if (time <= 3)
					text.text = "" + time;
			} else {
				text.text = "READY";

				if (time <= 3)
					text.text = "" + time;
			}
		} else {
			text.text = "FINISHED!";
		}
	}
}
