using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandedGOControll : MonoBehaviour {

	private Vector3 startPos;
	
	

	public JostickLeft jostickLandedGO;

	private Rigidbody rigidbody;



	private bool lerpingLJ = false;

	void Start () {
		Debug.Log("Start jostickLandedGO " );
		startPos = transform.position;
		rigidbody = GetComponent<Rigidbody>();
		// jostickLandedGO = FindObjectOfType<JostickLeft>();
	Debug.Log("jostickLandedGO is ___ " + jostickLandedGO );
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

			rigidbody.AddTorque(transform.up * -jostickX);
			rigidbody.AddTorque(transform.right * jostickZ);
			// Quaternion toRot = transform.rotation;

			Debug.Log("jostickLandedGO  X & Y  " + jostickX + " & " + jostickZ);

			// if(Mathf.Abs(jostickX) < 0.2 && Mathf.Abs(jostickZ) < 0.2)
			// return;


			// //Horizontal
			// if(Mathf.Abs(jostickX) > Mathf.Abs(jostickZ) )
			// {
				//x
					
				
				// if(jostickX > 0)
				// {	
					
				// 	toRot = Quaternion.AngleAxis (90, Vector3.back) * transform.rotation;
				// 	Debug.Log("Horizontal  toRot &  transform.rotation    " + toRot + " & " + transform.rotation);
				// }
				// else
				// {
					
				// 	toRot = Quaternion.AngleAxis (90, Vector3.forward) * transform.rotation;
				// 	Debug.Log("Horizontal  toRot &  transform.rotation    " + toRot + " & " + transform.rotation);
				// }

			// }
			//Vertical
			// else
			// {
			// 	//z
			// 	if(jostickZ > 0)
			// 	{
					
			// 		toRot = Quaternion.AngleAxis (90, Vector3.right) * transform.rotation;
			// 		Debug.Log("Vertical  toRot &  transform.rotation    " + toRot + " & " + transform.rotation);
			// 	}
			// 	else
			// 	{
					
			// 		toRot = Quaternion.AngleAxis (90, Vector3.left) * transform.rotation;
			// 		Debug.Log("jostVerticalickLandedGO  toRot &  transform.rotation    " + toRot + " & " + transform.rotation);
			// 	}
			// }

			// StartCoroutine(RotateLerp(transform.rotation, toRot));
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
