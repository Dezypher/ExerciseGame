using UnityEngine;
using System.Collections;

public class gameHandler : MonoBehaviour {

	public bool won = false;
	public int points;

	public int pointsToWin;

	void Update () {
		if (Input.GetButtonDown ("DebugWin")) {
			points++;
		}

		if (points >= pointsToWin) {
			won = true;
		}
	}
}
