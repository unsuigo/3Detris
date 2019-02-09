using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour {
	static int bestScore;

	void Save () {
		PlayerPrefs.SetInt ("score", GameManager.Instance.Score);
	}
	
	void Load () {
		bestScore = PlayerPrefs.GetInt("score");
	}
}
