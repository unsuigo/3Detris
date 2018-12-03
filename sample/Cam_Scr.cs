using UnityEngine;
using System.Collections;

public class Cam_Scr : MonoBehaviour {

    [Header("Основной скрипт")]
    public Main_Scr myMainScr; //основной скрипт, с него будем брать момент проигрыша
    [Header("Первая точка камеры")]
    public Transform myFirstDot; //первая точка камеры
    [Header("Вторая точка камеры")]
    public Transform mySecondDot; //вторая точка камеры

    [Header("Скорость перемещения камеры")]
    public float speed; //скорость движения камеры


    private Transform myTarget; //цель, к которой пойдет камера

    void Start () {
        myTarget = mySecondDot; //в начале целью камеры является вторая точка
    }
	
	void Update () {
        if (myMainScr.isLose == true) { //если игрок проиграл, то
            myTarget = myFirstDot; //целью является первая точка
        }

        transform.position = Vector3.Lerp(transform.position, myTarget.position, Time.deltaTime * speed); //плавное перемещение положения камеры от нынешней точки к цели
        transform.rotation = Quaternion.Lerp(transform.rotation, myTarget.rotation, Time.deltaTime * speed);//плавный поворот камеры
    }
}
