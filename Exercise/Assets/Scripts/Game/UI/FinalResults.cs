using UnityEngine;
using System.Collections;

public class FinalResults : MonoBehaviour {

	public UnityEngine.UI.Text  score;
	public UnityEngine.UI.Text  time;
	public UnityEngine.UI.Image stars;

	private ScoreRecorder scoreRecorder;

	// Use this for initialization
	void Start () {
		scoreRecorder = GameObject.Find ("ScoreRecorder").GetComponent<ScoreRecorder> ();
		
		scoreRecorder.SaveScore ();
		DisplayResults ();

		Destroy (GameObject.Find ("GameHandler"));
		Destroy (GameObject.Find ("Canvas"));
		Destroy (GameObject.Find ("CanvasFade"));
		Destroy (GameObject.Find ("Arduino Connection"));
	}

	void DisplayResults() {
		ArrayList results = scoreRecorder.stageResults;

		int sumScore = 0;
		int sumScoreMax = 0;
		float sumTimeSeconds = 0;

		for (int i = 0; i < results.Count; i++) {
			StageResult result = (StageResult) results [i];

			sumScore += result.score;
			sumScoreMax += result.maxScore;
			sumTimeSeconds += result.time;
		}

		score.text = sumScore + "/" + sumScoreMax;
		time.text = Mathf.Floor(sumTimeSeconds) + " seconds";
	}
}
