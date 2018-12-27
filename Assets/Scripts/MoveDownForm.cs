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
    // GameManager manager;
    // Use this for initialization
    void Start () {
        behaviour = GetComponent<TetrisBehaviour> ();
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
        if (Time.time - lastTime >= FindObjectOfType<GameManager> ().durationOneY) {

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
                    FindObjectOfType<GameLimitsZone> ().DeleteLayer ();

                    CalculateScore ();

                    
                    GameManager.Instance.UpdateLayerScore ();


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

        GameManager.score += itemScore * cubesInForm;
        Debug.Log ("SCORE to culculate   " + itemScore * cubesInForm);
        FindObjectOfType<UIManager> ().CurrentTimeScore(itemScore * cubesInForm);
       
    }

    public void OnButtonDown () {
        if (!GameManager.gamePaused) {
            // fallSpeed = GameLimitsZone.fall_speed;
            FindObjectOfType<GameManager> ().durationOneY = 0.01f;
           scoreDone = true;

        }
    }
}