using UnityEngine;
using System.Collections;

public enum ExerciseType {Hold, Many, Custom};
	
public class GameLogic : MonoBehaviour {

	public bool done = true;
	public int points;
	public int totalPoints;

	public float amtSeconds;
	public float currSeconds;
	public float interval;

	public float holdTime;
	public float timeHeld = 0;

	public int doAmt;
	public int amtDone = 0;

	public float stars;

	public bool resting = false;
	public bool canGetPoint = true;

	public bool success = false;
	public bool failed = false;

	public bool started = false;

	public string exerciseID;

	public bool isDoingExercise = false;
	public bool debugging;

	public int nextStage;

	public float elapsedTime = 0;

	public bool gameCompleted = false;

	public int scene;

	public bool pauseLogic = true;

	public ExerciseType exerciseType;

	//UI Variables

	public GameObject resultPanel;
	public ResultText resultScore;
	public ResultText resultTime;
	public GameObject canvas;

	private bool bufferNextStage = true;
	private StageSettings settings;
	private ScoreRecorder scoreRecorder;

	void Awake() {
		settings = ((GameObject)Resources.Load ("StageSettings")).GetComponent<StageSettings> ();
		scoreRecorder = GameObject.Find ("ScoreRecorder").GetComponent<ScoreRecorder> ();
		canvas = GameObject.Find ("Canvas");

		LoadStageSettings (settings.stageSettings [nextStage]);
	}

	void Update () {
		if (!pauseLogic) {		
			if (amtDone == doAmt && !bufferNextStage) {
				done = true;
				bufferNextStage = true;
				currSeconds = 3;
			}

			if (!done) {
				currSeconds -= Time.deltaTime;

				if (exerciseType != ExerciseType.Custom) {
					if (debugging) {
						if (Input.GetButton ("DebugWin")) {
							isDoingExercise = true;
						} else {
							isDoingExercise = false;
						}
					}

					if (isDoingExercise && canGetPoint) {
						timeHeld += Time.deltaTime;

						if (timeHeld >= holdTime) {
							GetPoint ();
							amtDone++;
							timeHeld = 0;
						}
					}
				}

				if (currSeconds <= 0) {
					if (!resting) {
						if (!success) {
							failed = true;
							if (started)
								amtDone++;
						}
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
					}
				}

				if (canGetPoint) {
					elapsedTime += Time.deltaTime;
				}
			} else {
				currSeconds -= Time.deltaTime;

				if (currSeconds <= 0) {
					bufferNextStage = true;

					currSeconds = 0;

					resultPanel.SetActive (true);
					resultScore.Display ();
					resultTime.Display ();
				}
			}
		}
	}

	public void LoadNextStage() {
		// Either Fade In and Out or do Cutscene

		pauseLogic = true;

		// Save Score
		scoreRecorder.AddScore(CalculateScore(), 100, elapsedTime);

		// Check if this is the last stage for the exercise set

		if (nextStage != -1) {
			// Fade In

			int thisScene = scene;

			LoadStageSettings (settings.stageSettings [nextStage]);

			if (thisScene != scene) { 
				UnityEngine.SceneManagement.SceneManager.LoadScene (scene);
			}
			// Fade Out


		} else {

			gameCompleted = true;

			// Load score screen

			UnityEngine.SceneManagement.SceneManager.LoadScene (2);
		}
	}

	public void StartStage() {
		done = false;
		bufferNextStage = false;
		pauseLogic = false;
	}

	public void Reset() {
		points = 0;

		currSeconds = 0;

		timeHeld = 0;
		amtDone = 0;
		stars = 0;

		resting = false;
		canGetPoint = true;

		success = false;
		failed = false;

		elapsedTime = 0;

		started = false;
		isDoingExercise = false;
	}

	public void LoadStageSettings(StageSetting settings) {
		Reset ();
		totalPoints = settings.totalPoints;
		amtSeconds = settings.amtSeconds;
		interval = settings.interval;
		holdTime = settings.holdTime;
		doAmt = settings.doAmt;
		nextStage = settings.nextStageIndex;
		scene = settings.sceneIndex;
		exerciseType = settings.exerciseType;

		//Instantiate TUTORIAL

		GameObject tutorial = (GameObject) Instantiate (settings.tutorial, canvas.transform);

		Vector3 newScale = tutorial.transform.localScale;

		newScale.x = 1;
		newScale.y = 1;
		newScale.z = 1;

		UnityEngine.RectTransform rectTransform = tutorial.GetComponent<UnityEngine.RectTransform> ();

		rectTransform.localPosition = new Vector2 (0, 0);
		tutorial.transform.localScale = newScale;
	}

	public void GetPoint() {
		if (canGetPoint) {
			points++;

			if (exerciseType != ExerciseType.Custom) {
				success = true;
				canGetPoint = false;

				if (exerciseType == ExerciseType.Hold)
					currSeconds = 0;
			}

			CalculateStars ();
		}
	}

	public int CalculateScore() {
		float score = 0;

		if (exerciseType == ExerciseType.Hold) {
			score = Mathf.Ceil((amtSeconds - elapsedTime) / (amtSeconds - holdTime) * 100);
		} else if (exerciseType == ExerciseType.Many) {
			score = amtDone / doAmt * 100;
		}

		return (int) score;
	}

	public void CalculateStars() {
		float pts = (float) points;
		float tPts = (float) totalPoints;

		stars = (float) (pts / tPts);
	}
}
