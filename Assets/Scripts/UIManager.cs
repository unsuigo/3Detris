using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public GameObject layersPanel;
	[SerializeField]
	private Text score_text;
	[SerializeField]
	private Text curentTimeScore_text;
	[SerializeField]
	private Text speed_text;

	public GameObject playPanel;
	public GameObject gameOverPanel;

	private bool isMuted = false;
	public bool isGameMode = true;

	#region Singleton
	private static UIManager instance;
	public static UIManager Instance {
		get {
			if (instance == null) {
				GameObject obj = GameObject.Find ("UIManager");
				if (obj != null) {
					instance = obj.AddComponent<UIManager> ();
				}

				if (instance == null) {
					if (obj == null)
						obj = new GameObject ();
					obj.name = typeof (UIManager).Name;

					instance = obj.AddComponent<UIManager> ();
				}
			}
			return instance;
		}
	}

	void Awake () {
		if (instance == null)
			instance = this as UIManager;
		else {
			Destroy (gameObject);
		}

	}
	#endregion 

	void Start () {
		GameManager.Instance.onScoreChanged += UpdateScore;
		GameManager.Instance.onCurrentScoreChanged += CurrentTimeScore;
	}

	public void switchGameMode () {
		print("in switch game mode");
		if (isGameMode) {
			playPanel.SetActive (false);
			layersPanel.SetActive (false);
			gameOverPanel.SetActive (true);
			isGameMode = false;
			print( "switch game mode to false");

		} else {
			playPanel.SetActive (true);
			layersPanel.SetActive (true);
			gameOverPanel.SetActive (false);
			isGameMode = true;
			print( "switch game mode to true");
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

		// Debug.Log ("Score is  " + GameManager.score.ToString () + "  speed is  " + GameManager.speed);

		score_text.text = GameManager.Instance.Score.ToString ();

		speed_text.text = GameManager.speed.ToString ();

		LayerPanelControll ();

	}

	public void UpdateScore (int score) {
		score_text.text = GameManager.Instance.Score.ToString ();
	}

	public void UpdateSpeed () {
		speed_text.text = GameManager.speed.ToString ();
	}

	void LayerPanelControll () {
		int layer = FindObjectOfType<GameLimitsZone> ().LastLayerHasItem ();
		for (int i = 0; i < layersPanel.transform.childCount - 1; i++) {
			// Debug.Log("Layer   ___ " + layer + " && i " + i);
			if (i <= layer)
				layersPanel.transform.GetChild (i).GetComponent<CanvasRenderer> ().SetAlpha (1f);
			else
				layersPanel.transform.GetChild (i).GetComponent<CanvasRenderer> ().SetAlpha (0f);

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
		Application.Quit ();
	}

	

	public void OnPlayBtn () {
		switchGameMode ();
		score_text.text = "0";
		FindObjectOfType<GameManager> ().PlayAgain ();
		UpdateUI ();
	}

}