using UnityEngine;
using System.Collections;

public class TimeLeft : MonoBehaviour {

	public GameLogic gameLogic;
	public UnityEngine.UI.Text text; 

	void Start () {
		text = GetComponent<UnityEngine.UI.Text> ();
	}

	void Update () {
		int time = Mathf.CeilToInt(gameLogic.currSeconds);

		if (gameLogic.canGetPoint)
			text.text = time + "s";
		else
			text.text = "";
	}
}
