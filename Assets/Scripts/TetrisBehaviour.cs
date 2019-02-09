using System.Collections;
using System.Collections.Generic;
// using CnControls;
using UnityEngine;

public class TetrisBehaviour : MonoBehaviour {

	private bool isPauseOn = false;

	private JostickLeft jostickL;

	private JostickRight jostickR;
	private bool lerpingRJ = false;
	private bool lerpingLJ = false;

	void Start () {
		jostickL = FindObjectOfType<JostickLeft> ();
		jostickR = FindObjectOfType<JostickRight> ();
	}

	void Update () {
		JostickLeft ();
		JostickRight ();
		// CheckUserInput ();

	}

	void JostickLeft () {
		// Debug.Log("CheckJostickRight() " );
		float jostickX;
		float jostickZ;

		if (jostickL.isPressed && !lerpingLJ) {

			jostickX = jostickL.HorizontalLeft ();
			jostickZ = jostickL.VerticalLeft ();
			Quaternion toRot = transform.rotation;

			Debug.Log ("Jostik  X & Y  " + jostickX + " & " + jostickZ);

			if (Mathf.Abs (jostickX) < 0.61f && Mathf.Abs (jostickZ) < 0.61f)
				return;

			//Horizontal
			if (Mathf.Abs (jostickX) > Mathf.Abs (jostickZ)) {
				//x

				if (jostickX > 0) {
					if (jostickR.isPressed)
						toRot = Quaternion.AngleAxis (90, Vector3.up) * transform.rotation;
					else
						toRot = Quaternion.AngleAxis (90, Vector3.back) * transform.rotation;
					// Debug.Log("toRot &  transform.rotation    " + toRot + " & " + transform.rotation);
				} else {
					if (jostickR.isPressed)
						toRot = Quaternion.AngleAxis (90, Vector3.down) * transform.rotation;
					else
						toRot = Quaternion.AngleAxis (90, Vector3.forward) * transform.rotation;
					// Debug.Log("toRot &  transform.rotation    " + toRot + " & " + transform.rotation);
				}

			}
			//Vertical
			else {
				//z
				if (jostickZ > 0) {
					if (jostickR.isPressed) return;
					toRot = Quaternion.AngleAxis (90, Vector3.right) * transform.rotation;
					// Debug.Log("toRot &  transform.rotation    " + toRot + " & " + transform.rotation);
				} else {
					if (jostickR.isPressed) return;
					toRot = Quaternion.AngleAxis (90, Vector3.left) * transform.rotation;
					// Debug.Log("toRot &  transform.rotation    " + toRot + " & " + transform.rotation);
				}
			}

			if (CheckZoneForRotate (toRot)) {
				StartCoroutine (RotateLerp (transform.rotation, toRot));
				FindObjectOfType<MyAudioManager> ().PlayClip ("Rotate");

			}

		}

	}

	void JostickRight () {

		// Debug.Log("CheckJostickRight() " );
		float jostickX;
		float jostickZ;

		if (jostickR.isPressed && !lerpingRJ) {

			jostickX = jostickR.HorizontalRight ();
			jostickZ = jostickR.VerticalRight ();
			Vector3 toPos = transform.position;

			// Debug.Log("Jostik  X & Y  " + jostickX + " & " + jostickZ);

			if (Mathf.Abs (jostickX) < 0.61f && Mathf.Abs (jostickZ) < 0.61f)
				return;

			//Horizontal
			if (Mathf.Abs (jostickX) > Mathf.Abs (jostickZ)) {
				//x

				if (jostickX > 0) {
					toPos += new Vector3 (1, 0, 0);
				} else {
					toPos += new Vector3 (-1, 0, 0);

				}

			}
			//Vertical
			else {
				//z
				if (jostickZ > 0) {
					toPos += new Vector3 (0, 0, 1);
				} else {
					toPos += new Vector3 (0, 0, -1);
				}
			}

			if (CheckZoneForMove (toPos)) {
				StartCoroutine (MoveLerp (transform.position, toPos));
				// FindObjectOfType<GameLimitsZone> ().UpdateZone (transform);
				MyAudioManager.Instance.PlayClip ("Move");
			}

		}
	}

	IEnumerator MoveLerp (Vector3 fromA, Vector3 toB) {
		lerpingRJ = true;
		float duration = 0.2f;
		for (float t = 0.0f; t < duration; t += Time.deltaTime) {
			transform.position = Vector3.Lerp (fromA, toB, t / duration);
			yield return new WaitForEndOfFrame ();
		}
		transform.position = toB;
		lerpingRJ = false;
		GameLimitsZone.Instance.UpdateZone (transform);
		// Debug.Log("Lerp End " + transform.position);
	}

	IEnumerator RotateLerp (Quaternion fromA, Quaternion toB) {
		lerpingLJ = true;
		float duration = 0.3f;
		for (float t = 0.0f; t < duration; t += Time.deltaTime) {
			transform.rotation = Quaternion.Lerp (fromA, toB, t / duration);
			yield return new WaitForEndOfFrame ();
		}
		transform.rotation = toB;
		lerpingLJ = false;
		GameLimitsZone.Instance.UpdateZone (transform);
		// Debug.Log("Lerp  Rotation End " + transform.rotation);
	}

	bool CheckZoneForMove (Vector3 newPos) {
		Vector3 oldPos = transform.position;
		transform.position = newPos;
		if (CheckIsValidPosition ()) {
			transform.position = oldPos;
			return true;
		} else
			transform.position = oldPos;

		return false;
	}
	bool CheckZoneForRotate (Quaternion newRot) {
		Quaternion oldRot = transform.rotation;
		transform.rotation = newRot;
		if (CheckIsValidPosition()) {
			transform.rotation = oldRot;
			return true;
		} else
			transform.rotation = oldRot;
			

		return false;
	}



	public bool CheckIsValidPosition() {
		

		foreach (Transform item in transform) {

			Vector3 pos = GameLimitsZone.Instance.Round (item.position);

				Debug.Log("CheckIsValidPosition   " +pos);

			if (GameLimitsZone.Instance.CheckIsInsideZone (pos) == false) {
				Debug.Log("CheckIsInsideZone   false " );
				return false;
			}

			if (GameLimitsZone.Instance.GetTransformZonePosition (pos) != null &&
				GameLimitsZone.Instance.GetTransformZonePosition (pos).parent != transform
			) {
				Debug.Log("CheckIsInsideZone   false " );
				return false;
			}
		}

		return true;
	}

}