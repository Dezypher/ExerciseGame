using UnityEngine;
using System.Collections;

public class Points : MonoBehaviour {

	public GameLogic gameLogic;
	public UnityEngine.UI.Text text; 

	void Start () {
		gameLogic = GameObject.Find ("GameHandler").GetComponent<GameLogic> ();
		text = GetComponent<UnityEngine.UI.Text> ();
	}

	void Update () {
		text.text = gameLogic.points + " PTS (" + gameLogic.amtDone + "/" + gameLogic.doAmt + ")";
	}
}
