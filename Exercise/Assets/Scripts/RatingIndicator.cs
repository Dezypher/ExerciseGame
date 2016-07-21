using UnityEngine;
using System.Collections;

public class RatingIndicator : MonoBehaviour {
	public GameLogic gameLogic;
	public GameObject[] stars;

	public void Start() {
		for (int i = 0; i < stars.Length; i++) {
			stars [i].SetActive (false);
		}
	}

	public void Update(){ 
		for (int i = 0; i < stars.Length; i++) {
			if(i + 1 <= (gameLogic.stars*5))
				stars [i].SetActive (true);
		}
	}
}
