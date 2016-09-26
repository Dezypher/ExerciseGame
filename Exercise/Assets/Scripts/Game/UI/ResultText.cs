using UnityEngine;
using System.Collections;

public class ResultText : MonoBehaviour {

	public enum ResultType { Score, Time }

	public ResultType type;

	private UnityEngine.UI.Text text;
	private GameLogic gameLogic;

	void Awake() {
		text = GetComponent<UnityEngine.UI.Text> ();
		gameLogic = GameObject.Find ("GameHandler").GetComponent<GameLogic> ();
	}

	public void Display() {
		if (type == ResultType.Score) {
			text.text = gameLogic.CalculateScore() + "/100";
		} else if (type == ResultType.Time) {
			text.text = ((int) gameLogic.elapsedTime) + " seconds";
		}
	}
}	
