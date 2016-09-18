using UnityEngine;
using System.Collections;

[System.Serializable]
public class StageSetting {
	public string name;
	public int totalPoints;
	public float amtSeconds;
	public float interval;
	public float holdTime;
	public int doAmt;
	public string exerciseID;
	public int sceneIndex;

	public int nextStageIndex;
}

public class StageSettings : MonoBehaviour {
	
	public StageSetting[] stageSettings;
}