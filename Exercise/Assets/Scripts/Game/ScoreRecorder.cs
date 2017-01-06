using UnityEngine;
using System.Collections;

public class StageResult {
	public int stageID;
	public int score;
	public int maxScore;
	public float time;
}

public class ScoreRecorder : MonoBehaviour {

	private DBHandler dbHandler;

	public int gameID;
	public ArrayList stageResults;

	void Awake() {
		stageResults = new ArrayList ();
	}

	public void AddScore(int stageID, int score, int maxScore, float time) {
		StageResult result = new StageResult ();

		result.score = score;
		result.maxScore = maxScore;
		result.time = time;

		stageResults.Add (result);

		Debug.Log ("Added score: " + score + " and time: " + time);
	}

	public void SaveScore() {
		StageResult[] results = new StageResult[stageResults.Count];

		for (int i = 0; i < results.Length; i++) {
			results [i] = (StageResult) stageResults [i];
		}

		dbHandler.InsertResults (gameID, results);
	}
}