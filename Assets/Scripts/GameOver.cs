using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI; 

public class GameOver:MonoBehaviour {
	
	static int scoreIs; 
	

	static int bestScoreIs = 0; 
	

	public Text bestScoreIs_text; 
	
	public	void OnGameOver () {

		// print("Game over");
		
		GameOverScore (); 
		
		GameObject[] leftCubes = GameObject.FindGameObjectsWithTag("Cube"); 

		foreach (GameObject cube in leftCubes) {
			cube.transform.parent = GameManager.Instance.landedCubesParent.transform; 
		}
			//  print("zone destroyed");
		 	Destroy(GameObject.FindGameObjectWithTag("Zone"));
			 MyAudioManager.instance.PlayClip("GameOver"); 

	}
	

	public  void GameOverScore() {
		scoreIs = GameManager.Instance.scoreGameOver; 
		bestScoreIs = PlayerPrefs.GetInt("bestScore"); 
		
		if (scoreIs > bestScoreIs) {
			bestScoreIs_text.text = scoreIs.ToString();
			Save (scoreIs); 
		}
		else
		{
		bestScoreIs_text.text = bestScoreIs.ToString (); 
			
		}
	}

	public void Save (int score) {
		PlayerPrefs.SetInt ("bestScore", scoreIs); 
	}

	void Load () {
		bestScoreIs = PlayerPrefs.GetInt("bestScore"); 
	}

	
}
