using UnityEngine;
using System.Collections;

public class ClimbBar : MonoBehaviour {

	public int side;

	private PlayerClimb playerClimb;
	private UnityEngine.UI.Image progressBar;

	void Awake() {
		playerClimb = GameObject.Find ("Player").GetComponent<PlayerClimb> ();
		progressBar = GetComponent<UnityEngine.UI.Image> ();
	}

	// Update is called once per frame
	void Update () {
		float fillAmount = 0;

		if (side == 0)
			fillAmount = playerClimb.timeHeldLeft / playerClimb.holdTimeLeft;

		if (side == 1)
			fillAmount = playerClimb.timeHeldRight / playerClimb.holdTimeRight;

		progressBar.fillAmount = fillAmount;
	}
}
