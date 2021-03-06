﻿using Detris;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonT<GameManager> {

	public GameObject landedCubesParent;
	public GameObject playingZonePrefab;
	public GameObject[] forms;
	public Vector3 startFormPosition;
	public float durationOneY;
	// private float startSpeed;

	public bool gamePaused = false;

	public int speed = 0;
	//Score
	public int scoreOneLayer = 50;
	public int scoreTwoLayers = 150;
	public int scoreThreeLayers = 400;

	// public static int score = 0;
	public int cubes = 0;
	public int layersTotal = 0;
	public int layersAtOnes = 0;

	public int scoreGameOver;
	public int cubesGameOver;
	public int layersGameOver;

#region  Observer
	private int score = 0;
	public int Score {
		get {
			return score;
		}
		set {
			score = value;
			if (onScoreChanged != null) {
				onScoreChanged (score);
			}

		}
	}
	public delegate void OnScoreChanged (int score);
	public event OnScoreChanged onScoreChanged;

	public delegate void OnCurrentScoreChanged (int currentScore);
	public event OnCurrentScoreChanged onCurrentScoreChanged;

	public delegate void OnSpeedChanged (int speed);
	public event OnSpeedChanged onSpeedChanged;

#endregion

	// #region Singleton
	// private static GameManager instance;
	// public static GameManager Instance {
	// 	get {
	// 		if (instance == null) {
	// 			GameObject obj = GameObject.Find ("GameManager");
	// 			if (obj != null) {
	// 				instance = obj.AddComponent<GameManager> ();
	// 			}

	// 			if (instance == null) {
	// 				if (obj == null)
	// 					obj = new GameObject ();
	// 				obj.name = typeof (GameManager).Name;

	// 				instance = obj.AddComponent<GameManager> ();
	// 			}
	// 		}
	// 		return instance;
	// 	}
	// }

	// void Awake () {
	// 	if (instance == null)
	// 		instance = this;
	// 	else {
	// 		Destroy (gameObject);
	// 		return;
	// 	}

	// }
	// #endregion

	void Start () {
		Instantiate (playingZonePrefab, new Vector3 (0, 0, 0), Quaternion.identity);
		SpawnNextItem ();
	}

	public void SpawnNextItem () {
		// print("spawn IN");
		if (UIManager.Instance.isGameMode == true) {
			// print("isGameMode == true");
			GameObject nextItem = Instantiate (GetRandomForm (forms), startFormPosition, Quaternion.identity);
			// durationOneY = startSpeed;
			durationOneY -= speed / 5;
		}
		UpdateSpeed ();
	}

	GameObject GetRandomForm (GameObject[] forms) {
		return forms[Random.Range (0, forms.Length - 1)];
	}

	public void GameOver () {
		scoreGameOver = score;
		// cubesGameOver = cubes;
		// layersGameOver = layersTotal;

		score = 0;
		speed = 0;
		// cubes = 0;
		// layersTotal = 0;
		FindObjectOfType<UIManager> ().switchGameMode ();
		FindObjectOfType<GameOver> ().OnGameOver ();
		// SceneManager.LoadScene("GameOver");
	}

	public void UpdateLayerScore (int layersAtOnes) {
		Debug.Log ("UpdateLevelScore   ?? " + layersAtOnes);
		if (layersAtOnes > 0) {
			if (layersAtOnes == 1) {
				GotOneLayer ();
			} else if (layersAtOnes == 2) {
				GotTwoLayers ();
			} else if (layersAtOnes == 3) {
				GotThreeLayers ();
			}
			// FindObjectOfType<UIManager>().UpdateUI();
			Debug.Log ("layersAtOnes   ?? " + layersAtOnes);
		}
	}

	public void UpdateCurrentScore (int currentScore) {
		if (onCurrentScoreChanged != null)
			onCurrentScoreChanged (currentScore);
	}

	public void OnDoneBtn () {
		if (!gamePaused) {

			FindObjectOfType<MoveDownForm> ().scoreDone = true;
			FindObjectOfType<MoveDownForm> ().fallOneLayerSpeed = 0.02f;
		}
	}

	private void GotOneLayer () {
		// UIManager.Instance.CurrentTimeScore (scoreOneLayer);
		onCurrentScoreChanged (scoreOneLayer);
		Score += scoreOneLayer;
		// Debug.Log ("GotOneLayer  ... " + scoreOneLayer + " and total " + score);
		MyAudioManager.Instance.PlayClip ("LayerDone1");

	}

	private void GotTwoLayers () {
		// FindObjectOfType<UIManager> ().CurrentTimeScore (scoreTwoLayers);
		Score += scoreTwoLayers;
		onCurrentScoreChanged (scoreTwoLayers);
		// Debug.Log ("GotTwoLayers  ?? " + scoreTwoLayers + " and total " + score);
		MyAudioManager.Instance.PlayClip ("LayerDone2");

	}

	private void GotThreeLayers () {
		// FindObjectOfType<UIManager> ().CurrentTimeScore (scoreThreeLayers);
		Score += scoreThreeLayers;
		onCurrentScoreChanged (scoreThreeLayers);
		// Debug.Log ("GotThreeLayers  ?? " + scoreThreeLayers + " and total " + score);
		MyAudioManager.Instance.PlayClip ("LayerDone3");

	}

	public void PlayAgain () {
		// print ("Play Again");
		landedCubesParent.transform.rotation = Quaternion.identity;

		foreach (Transform go in landedCubesParent.transform) {
			Destroy (go.gameObject);
		}

		GameObject playZone = Instantiate (playingZonePrefab, new Vector3 (0, 0, 0), Quaternion.identity);
		// print ("playingZone instance");
		// playZone.GetComponent<WallBehaviour> ().ResetWallMaterial ();
		GameLimitsZone.Instance.ResetZone ();
		SpawnNextItem ();

	}

	public void UpdateSpeed () {
		if (Score < 300) {
			if (speed < 0) {
				speed = 0;
				if (onSpeedChanged != null) {
					onSpeedChanged (speed);
				}
			}

		} else if (Score < 600) {
			if (speed < 1) {
				speed = 1;
				if (onSpeedChanged != null) {
					onSpeedChanged (speed);
				}
			}
		} else if (Score < 1000) {
			if (speed < 2) {
				speed = 2;
				if (onSpeedChanged != null) {
					onSpeedChanged (speed);
				}
			}
		} else if (Score < 1500) {
			if (speed < 3) {
				speed = 3;
				if (onSpeedChanged != null) {
					onSpeedChanged (speed);
				}
			}
		} else if (Score < 2000) {
			if (speed < 4) {
				speed = 4;
				if (onSpeedChanged != null) {
					onSpeedChanged (speed);
				}
			}
		} else if (Score < 3000) {
			if (speed < 5) {
				speed = 5;
				if (onSpeedChanged != null) {
					onSpeedChanged (speed);
				}
			}
		} else if (Score < 4000) {
			if (speed < 6) {
				speed = 6;
				if (onSpeedChanged != null) {
					onSpeedChanged (speed);
				}
			}
		} else if (Score < 5500) {
			if (speed < 7) {
				speed = 7;
				if (onSpeedChanged != null) {
					onSpeedChanged (speed);
				}
			}
		} else if (Score < 7000) {
			if (speed < 8) {
				speed = 8;
				if (onSpeedChanged != null) {
					onSpeedChanged (speed);
				}
			}

		} else if (Score < 9000) {
			if (speed < 9) {
				speed = 9;
				if (onSpeedChanged != null) {
					onSpeedChanged (speed);
				}
			}
		} else if (Score < 11000) {
			if (speed < 10) {
				speed = 10;
				if (onSpeedChanged != null) {
					onSpeedChanged (speed);
				}
			}
		}
		// Debug.Log ("SPEED  " + speed);

	}
}