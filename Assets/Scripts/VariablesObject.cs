﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariablesObject : MonoBehaviour {

	static int GameOverScore = 0;
	static int GameOverScoreTest = 0;



	void Update () {
		// CheckGameOverScore ();
	}

	public void CheckGameOverScore(){
		if(GameOverScore != GameManager.score){
			GameOverScore = GameManager.score;
			GameOverScoreTest = GameManager.scoreGameOver;

			Debug.Log ("scoreGameOverVar" + GameOverScore);
			Debug.Log ("scoreGameOverVartest  " + GameOverScoreTest);
		}

	}
}


