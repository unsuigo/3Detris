using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class JostickRight : MonoBehaviour , IDragHandler, IPointerUpHandler, IPointerDownHandler
 {

	public Image left;
    public Image right;
    private Image jostickBG;
    [SerializeField]
    private Image jostick;
    private Vector2 inputVector;
    public bool isPressed = false;

    Vector2 startPos;

    private void Start()
    {
        jostickBG = GetComponent<Image>();
        jostick = transform.GetChild(0).GetComponent<Image>();
        // startPos = transform.position;
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        //  #if UNITY_EDITOR

        

        // transform.position = Input.mousePosition;
        //  Debug.Log("Mouse position is  " + transform.position);
        // #endif
        
        //  Touch touch = Input.GetTouch(0);
        //  transform.position = touch.position;
        OnDrag(ped);
        isPressed = true;
        left.enabled = true;
        right.enabled = true;
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
       
        // transform.position =startPos;
        left.enabled = false;
        right.enabled = false;

        inputVector = Vector2.zero;
        jostick.rectTransform.anchoredPosition = Vector2.zero;
        isPressed = false;
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(jostickBG.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / jostickBG.rectTransform.sizeDelta.x);
            pos.y = (pos.y / jostickBG.rectTransform.sizeDelta.x);

           //print(pos);
            inputVector = new Vector2(pos.x * 2, pos.y * 2);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            jostick.rectTransform.anchoredPosition = 
                new Vector2(
                    inputVector.x * (jostickBG.rectTransform.sizeDelta.x / 2), 
                    inputVector.y * (jostickBG.rectTransform.sizeDelta.y / 2)
                );

        }
    }

    public float HorizontalRight()
    {
        print("HorizontalRight");
        if (inputVector.x != 0) return inputVector.x;
        else return Input.GetAxis("Horizontal");

    }

    public float VerticalRight()
    {

        print("VerticalRight");
        if (inputVector.y != 0) return inputVector.y;
        else return Input.GetAxis("Vertical");

    }
}
