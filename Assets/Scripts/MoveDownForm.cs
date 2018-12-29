using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDownForm : MonoBehaviour {

    private GameObject landedCubes;
    // private float durationOneStep  = 3f;
    public bool scoreDone;

    public float lastTime = 0.0f;
    public int itemScore = 12;
    private int cubesInForm;
    TetrisBehaviour behaviour;

    public float fallOneLayerSpeed;
    // GameManager manager;
    // Use this for initialization
    void Start () {
        behaviour = GetComponent<TetrisBehaviour> ();
       fallOneLayerSpeed = GameManager.Instance.durationOneY;
        //  manager = GetComponent<GameManager>();
        // landedCubes = GameObject.FindWithTag ("LandedParent");
    }

    void Update () {
        if (FindObjectOfType<UIManager> ().isGameMode == true)
            MoveDown ();
        else {
            Debug.Log ("Destroing last Form.....");
            Destroy (this.gameObject);
        }
    }

    void MoveDown () {

        //    Debug.Log("time....." + Time.time);

        // speed fall and landing
        if (Time.time - lastTime >= fallOneLayerSpeed) {

            // Debug.Log("time...IN");
            transform.position += new Vector3 (0, -1, 0);

            if (behaviour.CheckIsValidPosition ()) {

                FindObjectOfType<GameLimitsZone> ().UpdateZone (transform);
                // Debug.Log ("Space UpdateZoneShort done");

                if (itemScore > 0 && !scoreDone)
                    itemScore--;

            }

     //LANDED
            else {
                Debug.Log ("Landed.....");
                transform.position += new Vector3 (0, 1, 0);
                FindObjectOfType<MyAudioManager> ().PlayClip ("Landed");

                if (!FindObjectOfType<GameLimitsZone> ().CheckIsAboveZoneItems (transform)) {
                    GameManager.Instance.GameOver ();
                    Debug.Log ("Destroing last Form at first.....");
                    Destroy (this.gameObject);

                }

                if (UIManager.Instance.isGameMode) 
                {
                    SetLandedMaterial ();
                   

                    cubesInForm = transform.childCount;
                    transform.DetachChildren ();

                    CalculateScore ();
                    int layers = FindObjectOfType<GameLimitsZone> ().DeleteLayer ();
                    
                    if(layers > 0)
                    GameManager.Instance.UpdateLayerScore (layers);


                    // UIManager.Instance.UpdateUI ();


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
        GameManager.cubes += cubesInForm;
        Debug.Log ("cubes " + cubesInForm);
        int currentScore = itemScore * cubesInForm;
        GameManager.Instance.Score += currentScore;
        GameManager.Instance.UpdateCurrentScore(currentScore);
        Debug.Log ("SCORE to culculate   " + currentScore);

    }


    public void OnButtonDown () {
        if (!GameManager.gamePaused) {
            // fallSpeed = GameLimitsZone.fall_speed;
            FindObjectOfType<GameManager> ().durationOneY = 0.01f;
           scoreDone = true;

        }
    }
}