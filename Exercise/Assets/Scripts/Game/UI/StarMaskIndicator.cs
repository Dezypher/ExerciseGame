using UnityEngine;
using System.Collections;

public class StarMaskIndicator : MonoBehaviour {
	public GameLogic gameLogic;
	public RectTransform rect;

	public float finalWidth;

	public void Start() {
		gameLogic = GameObject.Find ("GameHandler").GetComponent<GameLogic> ();
		rect = GetComponent<RectTransform> ();
	}

	public void Update(){ 
		float percent = gameLogic.CalculateScore() / 100;

		rect.sizeDelta = new Vector2(finalWidth * percent, rect.sizeDelta.y);
	}
}
	