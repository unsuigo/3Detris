using System.Collections;
using System.Collections.Generic;
using Detris;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonT<UIManager> {

	public GameObject layersPanel;
	[SerializeField]
	private Text score_text;
	[SerializeField]
	private Text curentTimeScore_text;
	[SerializeField]
	private Text speed_text;

	public GameObject playPanel;
	public GameObject gameOverPanel;
	public GameObject quitPanel;

	private bool isMuted = false;
	public bool isGameMode = true;

	// #region Singleton
	// private static UIManager instance;
	// public static UIManager Instance {
	// 	get {
	// 		if (instance == null) {
	// 			GameObject obj = GameObject.Find ("UIManager");
	// 			if (obj != null) {
	// 				instance = obj.AddComponent<UIManager> ();
	// 			}

	// 			if (instance == null) {
	// 				if (obj == null)
	// 					obj = new GameObject ();
	// 				obj.name = typeof (UIManager).Name;

	// 				instance = obj.AddComponent<UIManager> ();
	// 			}
	// 		}
	// 		return instance;
	// 	}
	// }

	// void Awake () {
	// 	if (instance == null)
	// 		instance = this as UIManager;
	// 	else {
	// 		Destroy (gameObject);
	// 	}

	// }
	// #endregion 

	void Start () {
		GameManager.Instance.onScoreChanged += UpdateScore;
		GameManager.Instance.onCurrentScoreChanged += CurrentTimeScore;
		GameManager.Instance.onSpeedChanged += UpdateSpeed;
		LayerPanelControll ();

	}

	public void switchGameMode () {
		print ("in switch game mode");
		if (isGameMode) {
			playPanel.SetActive (false);
			layersPanel.SetActive (false);
			gameOverPanel.SetActive (true);
			isGameMode = false;
			print ("switch game mode to false");

		} else {
			playPanel.SetActive (true);
			layersPanel.SetActive (true);
			gameOverPanel.SetActive (false);
			isGameMode = true;
			print ("switch game mode to true");
		}

		Debug.Log ("Game Mode is ..... " + isGameMode);
		
	}
	public void OnPauseBtn () {
		if (GameManager.Instance.gamePaused) {
			Time.timeScale = 1;
			Debug.Log ("timer 1 ");
			GameManager.Instance.gamePaused = false;
		} else {
			Time.timeScale = 0;
			GameManager.Instance.gamePaused = true;
			Debug.Log ("timer 0 ");
		}
	}

	public void UpdateUI () {

		// Debug.Log ("Score is  " + GameManager.score.ToString () + "  speed is  " + GameManager.speed);

		// score_text.text = GameManager.Instance.Score.ToString ();

		speed_text.text = GameManager.Instance.speed.ToString ();

		LayerPanelControll ();

	}

	public void UpdateScore (int score) {
		score_text.text = score.ToString ();
	}

	public void UpdateSpeed (int speed) {
		speed_text.text = speed.ToString ();
	}

	void LayerPanelControll () {
		// print ("LayerPanelControll______");
		int layer = FindObjectOfType<GameLimitsZone> ().LastLayerHasItem ();
		for (int i = 0; i < layersPanel.transform.childCount - 1; i++) {
			// Debug.Log("Layer   ___ " + layer + " && i " + i);
			if (i <= layer) {
				layersPanel.transform.GetChild (i).GetComponent<CanvasRenderer> ().SetAlpha (1f);
				// print("___alpha 1");
			} else {
				layersPanel.transform.GetChild (i).GetComponent<CanvasRenderer> ().SetAlpha (0f);
				// print("__alpha 0");
			}
		}
	}

	// private int debugCurrentScoreCount = 0;
	public void CurrentTimeScore (int timeScore) {
		curentTimeScore_text.gameObject.SetActive (true);

		curentTimeScore_text.text = timeScore.ToString ();

		Invoke ("curentScoreOff", 1);
		// debugCurrentScoreCount++;
		// Debug.Log("curentTimeScore_text is " + timeScore + "  count " +debugCurrentScoreCount);
	}

	void curentScoreOff () {
		curentTimeScore_text.gameObject.SetActive (false);
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
		quitPanel.SetActive (true);

		//Pause
		Time.timeScale = 0;
		GameManager.Instance.gamePaused = true;
		// Debug.Log ("timer 0 ");
	}

	public void OnYesBtn () {
		FindObjectOfType<GameOver>().Save(GameManager.Instance.Score);
		Application.Quit ();
	}

	public void OnNoBtn () {
		quitPanel.SetActive (false);
		//Pause
		Time.timeScale = 1;
		GameManager.Instance.gamePaused = false;
		// Debug.Log ("timer 0 ");
	}

	public void OnPlayBtn () {
		switchGameMode ();
		score_text.text = "0";
		GameManager.Instance.PlayAgain ();
		UpdateUI ();
	}

}