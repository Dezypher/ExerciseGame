using UnityEngine;
using System.Collections;

public class StageResult {
	public int score;
	public int maxScore;
	public float time;
}

public class ScoreRecorder : MonoBehaviour {
	
	public ArrayList stageResults;
	void Awake() {
		stageResults = new ArrayList ();
	}

	public void AddScore(int score, int maxScore, float time) {
		StageResult result = new StageResult ();

		result.score = score;
		result.maxScore = maxScore;
		result.time = time;

		stageResults.Add (result);

		Debug.Log ("Added score: " + score + " and time: " + time);
	}
}