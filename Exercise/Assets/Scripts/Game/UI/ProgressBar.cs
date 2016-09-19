using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

	private GameLogic gameLogic;
	private UnityEngine.UI.Image progressBar;

	void Awake() {
		gameLogic = GameObject.Find ("GameHandler").GetComponent<GameLogic> ();
		progressBar = GetComponent<UnityEngine.UI.Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		float fillAmount = gameLogic.timeHeld / gameLogic.holdTime;

		progressBar.fillAmount = fillAmount;
	}
}
