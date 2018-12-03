using UnityEngine;
using System.Collections;

public class Quit_Scr : MonoBehaviour {

    void OnMouseEnter()
    {
        transform.localScale += new Vector3(0.001f, 0.001f, 0.001f);
    }

    void OnMouseExit()
    {
        transform.localScale -= new Vector3(0.001f, 0.001f, 0.001f);
    }

    void OnMouseUp()
    {
        Application.Quit();
    }
}
