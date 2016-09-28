using UnityEngine;
using System.Collections;

public class ProgressBarGlacial : MonoBehaviour {

	public int side;

	private PlayerGlacial playerGlacial;
	private UnityEngine.UI.Image progressBar;

	void Awake() {
		playerGlacial = GameObject.Find ("Player").GetComponent<PlayerGlacial> ();
		progressBar = GetComponent<UnityEngine.UI.Image> ();
	}

	// Update is called once per frame
	void Update () {
		float fillAmount = 0;

		if (side == 0)
			fillAmount = playerGlacial.leftHeld / playerGlacial.leftHoldAmt;

		if (side == 1)
			fillAmount = playerGlacial.rightHeld / playerGlacial.rightHoldAmt;

		progressBar.fillAmount = fillAmount;
	}
}
