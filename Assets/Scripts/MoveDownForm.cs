using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDownForm : MonoBehaviour {

    // private GameObject landedCubes;
  
    public bool scoreDone;

    public float lastTime = 0.0f;
    public int itemScore = 12;
    private int cubesInForm;
    TetrisBehaviour behaviour;
    GameLimitsZone zone;

    public float fallOneLayerSpeed;
    // GameManager manager;
    // Use this for initialization
    void Start () {
        behaviour = GetComponent<TetrisBehaviour> ();
       fallOneLayerSpeed = GameManager.Instance.durationOneY;
       zone = FindObjectOfType<GameLimitsZone> ();
        //  manager = GetComponent<GameManager>();
        // landedCubes = GameObject.FindWithTag ("LandedParent");
    }

    void Update () {
        if (FindObjectOfType<UIManager> ().isGameMode == true)
            MoveDown ();
        else {
            // Debug.Log ("Destroing last Form.....");
            Destroy (this.gameObject);
        }
    }

    void MoveDown () {

        // speed fall and landing
        if (Time.time - lastTime >= fallOneLayerSpeed) {

            // Debug.Log("time...IN");
            transform.position += new Vector3 (0, -1, 0);

            if (behaviour.CheckIsValidPosition ()) {

                zone.UpdateZone (transform);
                // Debug.Log ("Space UpdateZoneShort done");

                if (itemScore > 0 && !scoreDone)
                    itemScore--;

            }

            //LANDED
            else {
                Debug.Log ("Landed.....");
                transform.position += new Vector3 (0, 1, 0);
                FindObjectOfType<MyAudioManager> ().PlayClip ("Landed");

                if (!zone.CheckIsAboveZoneItems (transform)) {
                    GameManager.Instance.GameOver ();
                    Debug.Log ("Destroing last Form at first.....");
                    Destroy (this.gameObject);

                }

                if (UIManager.Instance.isGameMode) 
                {
                    SetLandedMaterial ();
                   

                    cubesInForm = transform.childCount;
                    transform.DetachChildren ();
                    // Destroy(GameObject.FindGameObjectWithTag("Zone"));

                    CalculateScore ();
                    int layers = FindObjectOfType<GameLimitsZone> ().DeleteLayer ();
                    
                    if(layers > 0)
                    GameManager.Instance.UpdateLayerScore (layers);


                    UIManager.Instance.UpdateUI ();


                    GameManager.Instance.SpawnNextItem ();

                }

                Destroy (this.gameObject);
            }

            lastTime = Time.time;
            // Debug.Log("time....." +lastTime);

        }
    }

    private void SetLandedMaterial () {
         foreach (Transform item in transform) {
                        Vector3 pos = FindObjectOfType<GameLimitsZone> ().Round (item.position);

                        // set up material of layer to the child when it landed
                        Material newMat;
                        Renderer renderer = item.GetComponent<MeshRenderer> ();
                        newMat = FindObjectOfType<Grafics> ().SetMaterialDown ((int) pos.y);
                        renderer.material = newMat;
                        item.gameObject.tag = "Cube";

                    }
    }
    
    private void CalculateScore () {
        GameManager.Instance.cubes += cubesInForm;
        // Debug.Log ("cubes " + cubesInForm);
        int currentScore = itemScore * cubesInForm;
        GameManager.Instance.Score += currentScore;
        GameManager.Instance.UpdateCurrentScore(currentScore);
        // Debug.Log ("SCORE to culculate   " + currentScore);

    }


}