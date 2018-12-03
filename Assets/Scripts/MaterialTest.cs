using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Renderer renderer = GetComponent<Renderer>();
		Material newMat;
		newMat = Resources.Load ("Materials/BlackMat", typeof(Material)) as Material;
		renderer.material = newMat;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
