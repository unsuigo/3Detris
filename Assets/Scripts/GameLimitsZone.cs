using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 

// using UnityEngine.UI;
// using UnityEngine.SceneManagement;

public class GameLimitsZone:MonoBehaviour {

	private static int zoneWidth = 5; 
	private static int zoneDeep = 5; 
	private static int zoneHeight = 11; 
	private Material newMat; 

	public static Transform[, , ] zone = new Transform[zoneWidth, zoneHeight, zoneDeep]; 


		public bool CheckIsAboveZoneItems(Transform form) {

				foreach (Transform item in form) {
					Vector3 pos = Round (item.position); 

					if (pos.y < zoneHeight - 1) {
						return true; 
					}
				}

		return false; 
	}

		public int LastLayerHasItem()
		{
			int layer = -1;
			
			for(int i = 0; i < zoneHeight; i++)
			{
				if(LayerHasItem (i))
				{
					 layer++;

				}
			}
			return layer;
		}
public void DeleteLayer () {
		int layers = 0; 
		for (int y = 0; y < zoneHeight; y++) {
			if (IsFullLayer(y)) {
				DeleteLayerItems (y); 
				layers++; 
				MoveAllLayersDown (y + 1); --y; 
			}
		}
		GameManager.layersAtOnes = layers; 
	}

	public void DeleteLayerItems (int y) {
		for (int x = 0; x < zoneWidth; x++) {
			for (int z = 0; z < zoneDeep; z++) {
				Destroy (zone[x, y, z].gameObject); 
				zone [x, y, z] = null; 
			}
		}
	}

	public bool LayerHasItem (int y) {
		for (int x = 0; x < zoneWidth; x++) {
			for (int z = 0; z < zoneDeep; z++) {
				if (zone[x, y, z] != null) {
					
					return true; 
				}
			}
		}

		// numberOfRowsInTurn++;
		return false; 
	}

	public bool IsFullLayer (int y) {
		for (int x = 0; x < zoneWidth; x++) {
			for (int z = 0; z < zoneDeep; z++) {
				if (zone[x, y, z] == null) {
					
					return false; 
				}
			}
		}

		// numberOfRowsInTurn++;
		return true; 
	}



	public void MoveLayerDown (int y) {
//		Material newMat;

		for (int x = 0; x < zoneWidth; x++) {
			for (int z = 0; z < zoneDeep; z++) {
				
				if (zone[x, y, z] != null) {
					
					Renderer renderer = zone [x, y, z]. GetComponent < MeshRenderer > (); 
					newMat = FindObjectOfType <Grafics> ().SetMaterialDown (y-1); 
					renderer.material = newMat; 

//					Debug.Log ("MoveLayerDown  " +x+y+z);
					zone[x, y-1, z] = zone[x, y, z]; 
					zone [x, y, z] = null; 
					zone [x, y - 1, z].position += new Vector3 (0, -1, 0); 

				}
			}
		}
	}


	public void MoveAllLayersDown (int y) {
		for (int i = y; i < zoneHeight; i++) {
			MoveLayerDown (i); 
		}
	}

public void ResetZone()
{
	zone = new Transform[zoneWidth, zoneHeight, zoneDeep];
}

		public void UpdateZone (Transform form) {
		
		for (int y = 0; y < zoneHeight; ++y) {
			for (int x = 0; x < zoneWidth; ++x) {
				for (int z = 0; z < zoneDeep; ++z) {

					if (zone [x, y, z] != null) {
						
						if (zone [x, y, z].parent == form) {
							FindObjectOfType < WallBehaviour > ().CleanShadow ((int)x, (int)y, (int)z); 
							zone [x, y, z] = null; 
						}
					}
				}
			}
		}



		foreach (Transform item in form.transform) {
			Vector3 pos = Round (item.position); 
			if (pos.y < zoneHeight) {
				zone [(int)pos.x, (int)pos.y, (int)pos.z] = item; 

				FindObjectOfType < WallBehaviour > ().Shadow ((int)pos.x, (int)pos.y, (int)pos.z); 
		
			}
		}
	}


	
	public Transform GetTransformZonePosition (Vector3 pos) {
		if (pos.y > zoneHeight + 1) {
			return null; 
		}else {
//									Debug.Log ("GetTransformZonePosition   " +pos);
			return zone [(int)pos.x, (int)pos.y, (int)pos.z]; 

		}
	}



	public bool CheckIsInsideZone (Vector3 pos) {

		return ((int)pos.x >= 0 && (int)pos.x < zoneWidth && (int)pos.z >= 0 && (int)pos.z < zoneDeep && (int)pos.y >= 0); 
	}

	public Vector3 Round(Vector3 pos) {
		return new Vector3 (Mathf.Round(pos.x), 
							Mathf.Round(pos.y), 
							Mathf.Round(pos.z)); 
	}


}




