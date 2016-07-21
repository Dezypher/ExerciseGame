using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

	public bool done = false;
	public int points;
	public int totalPoints;

	public float amtSeconds;
	public float currSeconds;
	public float interval;

	public int doAmt;
	public int amtDone = 0;

	public float stars;

	public bool resting = false;
	public bool canGetPoint = true;

	public bool success = false;
	public bool failed = false;

	public bool started = false;

	void Update () {
		currSeconds -= Time.deltaTime;

		if (amtDone == doAmt) {
			done = true;
		}

		if (!done) {
			if (Input.GetButtonDown ("DebugWin") && canGetPoint) {
				points++;
				success = true;
				canGetPoint = false;
				currSeconds = 0;

				CalculateStars ();
			}

			if (currSeconds <= 0) {
				if (!resting) {
					if (!success)
						failed = true;
					resting = true;
					canGetPoint = false;
					currSeconds = interval;
				} else {
					if (!started)
						started = true;
					success = false;
					failed = false;
					resting = false;
					canGetPoint = true;
					currSeconds = amtSeconds;
					amtDone++;
				}
			}
		}
	}

	public void CalculateStars() {
		float pts = (float) points;
		float tPts = (float) totalPoints;

		stars = (float) (pts / tPts);
	}
}
