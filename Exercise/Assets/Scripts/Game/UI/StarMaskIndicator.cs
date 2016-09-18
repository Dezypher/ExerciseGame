using UnityEngine;
using System.Collections;

public class StarMaskIndicator : MonoBehaviour {
	public GameLogic gameLogic;
	public RectTransform rect;

	public float finalWidth;

	public void Start() {
		rect = GetComponent<RectTransform> ();
	}

	public void Update(){ 
		float percent = gameLogic.stars;

		rect.sizeDelta = new Vector2(finalWidth * percent, rect.sizeDelta.y);
	}
}
	