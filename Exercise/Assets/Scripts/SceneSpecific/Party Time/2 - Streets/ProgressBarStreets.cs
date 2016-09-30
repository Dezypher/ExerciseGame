using UnityEngine;
using System.Collections;

public class ProgressBarStreets : MonoBehaviour {

	public int side;

	private PlayerControllerTire player;
	private UnityEngine.UI.Image progressBar;

	void Awake() {
		player = GameObject.Find ("Player").GetComponent<PlayerControllerTire> ();
		progressBar = GetComponent<UnityEngine.UI.Image> ();
	}

	// Update is called once per frame
	void Update () {
		float fillAmount = 0;

		if (side == 0)
			fillAmount = player.leftHeld / player.leftHoldAmt;

		if (side == 1)
			fillAmount = player.rightHeld / player.rightHoldAmt;

		progressBar.fillAmount = fillAmount;
	}
}
