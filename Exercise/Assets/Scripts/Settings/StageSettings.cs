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
	public string[] exerciseIDs;
	public int sceneIndex;
	public bool endAfterTimeOut;
	public bool recordEffortPoints;
	public ExerciseType exerciseType;

	public GameObject tutorial;

	public int nextStageIndex;
}

public class StageSettings : MonoBehaviour {
	
	public StageSetting[] stageSettings;
}