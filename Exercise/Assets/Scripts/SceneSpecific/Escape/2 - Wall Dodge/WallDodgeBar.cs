using UnityEngine;
using System.Collections;

public class WallDodgeBar : MonoBehaviour {

	public int side;

	private PlayerWallDodge playerWallDodge;
	private UnityEngine.UI.Image progressBar;

	void Awake() {
		playerWallDodge = GameObject.Find ("Player").GetComponent<PlayerWallDodge> ();
		progressBar = GetComponent<UnityEngine.UI.Image> ();
	}

	// Update is called once per frame
	void Update () {
		float fillAmount = 0;

		if (side == 0)
			fillAmount = playerWallDodge.timeHeldLeft / playerWallDodge.holdTimeLeft;

		if (side == 1)
			fillAmount = playerWallDodge.timeHeldRight / playerWallDodge.holdTimeRight;

		progressBar.fillAmount = fillAmount;
	}
}
