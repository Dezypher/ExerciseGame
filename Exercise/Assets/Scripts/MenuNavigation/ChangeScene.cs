using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	public int sceneNumber;

	public void StartSceneChange() {
		SceneManager.LoadScene (sceneNumber);
	}
}
