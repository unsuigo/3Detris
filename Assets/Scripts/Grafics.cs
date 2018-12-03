using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grafics : MonoBehaviour {
	Material newMat;
	

	public Material  SetMaterial (int y){
		
//		Material newMat;
		newMat = Resources.Load ("Materials/GlassMat 0", typeof(Material)) as Material;
		int LayerY = y;

		switch (LayerY){

		case 11:
			newMat = Resources.Load ("Materials/GlassMat 11", typeof(Material)) as Material;
			break;

		case 10:
			newMat = Resources.Load ("Materials/GlassMat 10", typeof(Material)) as Material;
			break;

		case 9:
			newMat = Resources.Load ("Materials/GlassMat 9", typeof(Material)) as Material;
			break;

		case 8:
			newMat = Resources.Load ("Materials/GlassMat 8", typeof(Material)) as Material;
			break;

		case 7:
			newMat = Resources.Load ("Materials/GlassMat 7", typeof(Material)) as Material;
			break;

		case 6:
			newMat = Resources.Load ("Materials/GlassMat 6", typeof(Material)) as Material;
			break;

		case 5:
			newMat = Resources.Load ("Materials/GlassMat 5", typeof(Material)) as Material;
			break;

		case 4:
			newMat = Resources.Load ("Materials/GlassMat 4", typeof(Material)) as Material;
			break;

		case 3:
			newMat = Resources.Load ("Materials/GlassMat 3", typeof(Material)) as Material;
			break;

		case 2:
			newMat = Resources.Load ("Materials/GlassMat 2", typeof(Material)) as Material;
			break;

		case 1:
			newMat = Resources.Load ("Materials/GlassMat 1", typeof(Material)) as Material;
			break;

		case 0:
			newMat = Resources.Load ("Materials/GlassMat 0", typeof(Material)) as Material;
			break;
		}


		return newMat;
	}

	public Material  SetMaterialDown (int y){

		newMat = Resources.Load ("Materials/Mat 0", typeof(Material)) as Material;
		int LayerY = y;

		switch (LayerY){

		case 11:
			newMat = Resources.Load ("Materials/Mat 11", typeof(Material)) as Material;
			break;

		case 10:
			newMat = Resources.Load ("Materials/Mat 10", typeof(Material)) as Material;
			break;

		case 9:
			newMat = Resources.Load ("Materials/Mat 9", typeof(Material)) as Material;
			break;

		case 8:
			newMat = Resources.Load ("Materials/Mat 8", typeof(Material)) as Material;
			break;

		case 7:
			newMat = Resources.Load ("Materials/Mat 7", typeof(Material)) as Material;
			break;

		case 6:
			newMat = Resources.Load ("Materials/Mat 6", typeof(Material)) as Material;
			break;

		case 5:
			newMat = Resources.Load ("Materials/Mat 5", typeof(Material)) as Material;
			break;

		case 4:
			newMat = Resources.Load ("Materials/Mat 4", typeof(Material)) as Material;
			break;

		case 3:
			newMat = Resources.Load ("Materials/Mat 3", typeof(Material)) as Material;
			break;

		case 2:
			newMat = Resources.Load ("Materials/Mat 2", typeof(Material)) as Material;
			break;

		case 1:
			newMat = Resources.Load ("Materials/Mat 1", typeof(Material)) as Material;
			break;

		case 0:
			newMat = Resources.Load ("Materials/Mat 0", typeof(Material)) as Material;
			break;
		}


		return newMat;
	}
}