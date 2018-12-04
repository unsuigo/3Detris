using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public GameObject layersPanel;
	public Text score_text;

	public Text curentTimeScore_text;

	public Text speed_text;

	public GameObject playPanel;
	public GameObject gameOverPanel;

	private bool isMuted = false;
	public bool isGameMode = true;

	public static UIManager instance;

	void Awake () {
		if (instance == null)
			instance = this;
		else {
			Destroy (gameObject);
			return;
		}

	}

	public void switchGameMode () {
		if (isGameMode) {
			playPanel.SetActive (false);
			layersPanel.SetActive (false);
			gameOverPanel.SetActive (true);
			isGameMode = false;

		} else {
			playPanel.SetActive (true);
			layersPanel.SetActive (true);
			gameOverPanel.SetActive (false);
			isGameMode = true;
		}

		Debug.Log ("Game Mode is ..... " + isGameMode);
	}
	public void OnPauseBtn () {
		if (GameManager.gamePaused) {
			Time.timeScale = 1;
			Debug.Log ("timer 1 ");
			GameManager.gamePaused = false;
		} else {
			Time.timeScale = 0;
			GameManager.gamePaused = true;
			Debug.Log ("timer 0 ");
		}
	}

	public void UpdateUI () {

		Debug.Log ("Score is  " + GameManager.score.ToString ());
		string scoreTxt = GameManager.score.ToString ();
		score_text.text = scoreTxt;
		LayerPanelControll ();

	}

	void LayerPanelControll () {
		int layer = FindObjectOfType<GameLimitsZone> ().LastLayerHasItem ();
		for (int i = 0; i < layersPanel.transform.childCount-1; i++) {
			Debug.Log("Layer   ___ " + layer + " && i " + i);
			if(i <= layer)
			layersPanel.transform.GetChild(i).GetComponent<CanvasRenderer>().SetAlpha(1f);
			else
			layersPanel.transform.GetChild(i).GetComponent<CanvasRenderer>().SetAlpha(0f);

		}
	}

	public void CurrentTimeScore(int timeScore)
	{
		curentTimeScore_text.gameObject.SetActive(true);
		 
		curentTimeScore_text.text = timeScore.ToString ();

		Invoke("curentScoreOff", 1);
	}

	void curentScoreOff()
	{
		curentTimeScore_text.gameObject.SetActive(false);
	}

	public void OnMusicBtn () {
		if (!isMuted) {
			FindObjectOfType<MyAudioManager> ().StopClip ("BackGround");
			isMuted = true;
		} else {
			FindObjectOfType<MyAudioManager> ().PlayClip ("BackGround");
			isMuted = false;
		}

	}


	public void OnExitBtn () {
		Application.Quit ();
	}

	public void OnDoneBtn () {
		if (!GameManager.gamePaused) {
			// fallSpeed = GameLimitsZone.fall_speed;
			FindObjectOfType<GameManager> ().durationOneY = 0.01f;
			FindObjectOfType<MoveDownForm> ().scoreDone = true;
		}
	}

	public void OnPlayBtn () {
		switchGameMode ();
		score_text.text = "0";
		FindObjectOfType<GameManager> ().PlayAgain ();
		UpdateUI();
	}

}