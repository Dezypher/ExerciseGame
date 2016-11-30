using UnityEngine;
using System.Collections;

public class SwingBar : MonoBehaviour {

	private PlayerRopeSwing playerRopeSwing;
	private UnityEngine.UI.Image progressBar;

	void Awake() {
		playerRopeSwing = GameObject.Find ("Player").GetComponent<PlayerRopeSwing> ();
		progressBar = GetComponent<UnityEngine.UI.Image> ();
	}

	// Update is called once per frame
	void Update () {
		float fillAmount = 0;

		fillAmount = playerRopeSwing.timeHeld / playerRopeSwing.holdTime;

		progressBar.fillAmount = fillAmount;
	}
}
