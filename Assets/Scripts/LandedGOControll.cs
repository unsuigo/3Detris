using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandedGOControll : MonoBehaviour {

	private Vector3 startPos;
	
	

	public JostickLeft jostickLandedGO;

	private Rigidbody rb;



	private bool lerpingLJ = false;

	void Start () {
		// Debug.Log("Start jostickLandedGO " );
		startPos = transform.position;
		rb = GetComponent<Rigidbody>();
		// jostickLandedGO = FindObjectOfType<JostickLeft>();
	// Debug.Log("jostickLandedGO is ___ " + jostickLandedGO );
	}

	void Update () {

		LandedGOContr();
		
	}


	void LandedGOContr()
	{
		// Debug.Log("CheckJostickRight() " );
		float jostickX; 
		float jostickZ;

		if(jostickLandedGO.isPressed && !lerpingLJ ){


			jostickX =  jostickLandedGO.HorizontalLeft();
			jostickZ =  jostickLandedGO.VerticalLeft();

			rb.AddTorque(transform.up * -jostickX);
			rb.AddTorque(transform.right * jostickZ);
			// Quaternion toRot = transform.rotation;

			// Debug.Log("jostickLandedGO  X & Y  " + jostickX + " & " + jostickZ);

	
			FindObjectOfType<MyAudioManager>().PlayClip("Rotate");
			
		}


	}



	IEnumerator MoveLerp(Vector3 fromA , Vector3 toB)
	{
		
		float duration   = 0.2f;
		for (float t=0.0f; t < duration; t +=Time.deltaTime) {
		transform.position = Vector3.Lerp(fromA,toB, t/duration);
		yield return new WaitForEndOfFrame();
		}
		transform.position = toB;
		
			// Debug.Log("Lerp End " + transform.position);
	}

	IEnumerator RotateLerp(Quaternion fromA, Quaternion toB)
	{
		lerpingLJ = true;
		float duration   = 0.3f;
		for (float t=0.0f; t<duration; t+=Time.deltaTime) {
		transform.rotation = Quaternion.Lerp(fromA,toB, t/duration);
		yield return new WaitForEndOfFrame();
		}
		transform.rotation = toB;
		lerpingLJ = false;
			// Debug.Log("Lerp  Rotation End " + transform.rotation);
	}



}
