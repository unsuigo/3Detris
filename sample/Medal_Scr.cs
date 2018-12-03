using UnityEngine;
using System.Collections;

public class Medal_Scr : MonoBehaviour {

    [Header("Префаб пуговицы")]
    public GameObject prefab; //префаб пуговицы

	public void spawn (int numbers) { // функция падения пуговицы (количество пуговиц)
        for (int i = 0; i < numbers; i++) { //перебираем количество пуговиц
            GameObject medal = Instantiate(prefab); //создаем локальную перменную, которая будет новой пуговицей и в ней создаем префаб
            medal.transform.position = transform.position + Vector3.up * i * 2f; // настриваем положение пуговицы
            medal.transform.rotation = transform.rotation; //настраиваем поворот пуговицы
            int randomNum = Random.Range(0, 7); //случайная величина, отвечающая за цвет пуговицы (или за что-то еще например)
            if (randomNum == 0) { //если 0, то цвет голубой и т.д.
                medal.GetComponent<Renderer>().material.color = Color.blue;
            }
            else if (randomNum == 1) {
                medal.GetComponent<Renderer>().material.color = Color.yellow;
            }
            else if (randomNum == 2)
            {
                medal.GetComponent<Renderer>().material.color = Color.red;
            }
            else if (randomNum == 3)
            {
                medal.GetComponent<Renderer>().material.color = Color.cyan;
            }
            else if (randomNum == 4)
            {
                medal.GetComponent<Renderer>().material.color = Color.green;
            }
            else if (randomNum == 5)
            {
                medal.GetComponent<Renderer>().material.color = Color.magenta;
            }
            else
            {
                medal.GetComponent<Renderer>().material.color = Color.black;
            }
            
        }
        
	}

}
