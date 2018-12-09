
using UnityEngine; 
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


	public GameObject landedCubesParent; 
	public GameObject playingZone; 
	public GameObject[] forms; 
	public Vector3 startFormPosition; 
	public float durationOneY;
	private float startSpeed;

	public static bool gamePaused = false;
	
	public static int speed = 0;
	//Score
	public static int scoreOneLayer = 50;
	public static int scoreTwoLayers = 150;
	public static int scoreThreeLayers = 400;
	
	public static int score = 0;
	public static int cubes = 0;
	public static int layersTotal = 0;
	public static int layersAtOnes = 0;

	public static int scoreGameOver;
	public static int cubesGameOver;
	public static int layersGameOver;


 public static GameManager instance;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject); 
            return;
        }

	}
	
	void Start () {
		// FindObjectOfType<UIManager>().UpdateUI();
		startSpeed = durationOneY-(speed /5);
		SpawnNextItem (); 
	}

	
	public void SpawnNextItem () {
		if(FindObjectOfType<UIManager>().isGameMode == true){

		GameObject nextItem = Instantiate(GetRandomForm(forms), startFormPosition, Quaternion.identity); 
		durationOneY = startSpeed;
		durationOneY -=speed/5;
		}
	}

	
	GameObject GetRandomForm (GameObject[] forms)
	{
		return forms[Random.Range(0, forms.Length-1)] ;
	}


	public void GameOver (){
		scoreGameOver = score;
		// cubesGameOver = cubes;
		// layersGameOver = layersTotal;

		score = 0;
		speed = 0;
		// cubes = 0;
		// layersTotal = 0;
		FindObjectOfType<UIManager>().switchGameMode();
		FindObjectOfType<GameOver>().OnGameOver();
		// SceneManager.LoadScene("GameOver");
	}


	public void UpdateLayerScore ()
	{
		Debug.Log("UpdateLevelScore   ?? " + layersAtOnes );	
		if(layersAtOnes > 0)
		{
			if (layersAtOnes == 1) 
			{
				GotOneLayer ();
			}
			 else if (layersAtOnes == 2) 
			{
				GotTwoLayers ();
			} 
			else if (layersAtOnes == 3)
			{
				GotThreeLayers ();
			}
  		// FindObjectOfType<UIManager>().UpdateUI();
		Debug.Log("layersAtOnes   ?? " + layersAtOnes );	
		}
	}

	private void GotOneLayer (){
		FindObjectOfType<UIManager>().CurrentTimeScore(scoreOneLayer);
		score += scoreOneLayer;
		Debug.Log("GotOneLayer  ... " + scoreOneLayer + " and total " + score);
		FindObjectOfType<MyAudioManager>().PlayClip("LayerDone1");
		
		
	}

	private void GotTwoLayers (){
		FindObjectOfType<UIManager>().CurrentTimeScore(scoreTwoLayers);
		score += scoreTwoLayers;
		Debug.Log("GotTwoLayers  ?? " + scoreTwoLayers + " and total " + score);
		FindObjectOfType<MyAudioManager>().PlayClip("LayerDone2");
		
	}

	private void GotThreeLayers (){
		FindObjectOfType<UIManager>().CurrentTimeScore(scoreThreeLayers);
		score += scoreThreeLayers;
		Debug.Log("GotThreeLayers  ?? " + scoreThreeLayers + " and total " + score);
		FindObjectOfType<MyAudioManager>().PlayClip("LayerDone3");
		
	}

	public void PlayAgain()
	{
		
		landedCubesParent.transform.rotation = Quaternion.identity;
		foreach(Transform go in landedCubesParent.transform)
		{
			Destroy(go.gameObject);
		}

		playingZone.SetActive(true);

		FindObjectOfType<WallBehaviour> ().ResetWallMaterial();
		FindObjectOfType<GameLimitsZone> ().ResetZone();
		SpawnNextItem();

	}


}
