using UnityEngine;
using System.Collections;

public class ScoreRecorder : MonoBehaviour {
	
	public ArrayList stageResults;

	class StageResult {
		public int score;
		public float time;
	}

	void Awake() {
		stageResults = new ArrayList ();
	}

	public void AddScore(int score, float time) {
		StageResult result = new StageResult ();

		result.score = score;
		result.time = time;

		stageResults.Add (result);

		Debug.Log ("Added score: " + score + " and time: " + time);
	}
}