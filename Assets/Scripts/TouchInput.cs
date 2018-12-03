using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour {

	public GameObject joystickL;
	public GameObject joystickR;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		for(int i = 0; i < Input.touchCount; i++)
		{
			Vector2 tPosition = Input.touches[i].position;
			if(tPosition.x < 500f)
			{
				//left

				joystickL.transform.position = tPosition;
			}

			if(tPosition.x > 800f)
			{
				//right
				joystickR.transform.position = tPosition;
				
			}
		}
		
	}
}
