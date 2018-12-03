using UnityEngine;
using System.Collections;

public class Main_Scr : MonoBehaviour {

    [Header ("Ширина сцены (без границ)")]
    public int sceneWidth; // ширина игровой области (без учета границ)
    [Header("Высота сцены (без границ)")]
    public int sceneHeight; // высота игровой области (без учета границ)
    [Header("Материал для объектов")]
    public Material mainMat; // материал, который будет присвоен кубикам
    [Header("Префаб кубика")]
    public GameObject CubePref; // префаб, который будет играть роль кубика в падающей фигуре
    [Header("Скрипт падения пуговицы")]
    public Medal_Scr myMedalScr; // скрипт, который запускает падение пуговицы в банку
    int[,] _scene; //массив для обозгачения заполненных участков сцены (0 - занято, 1 - свободно)

    GameObject CubesPivot; //родительский объект для упавших кубиков
    int CubesPivotChilds; // количество дочерних объектов в предыдущем объекте (CubesPivot)

    int nextFigure; //переменная для случайного выбора следующей фигуры

    Transform actFigure; ////родительский объект для падающих кубиков
    Transform demoFigure;//родительский объект для демонстрационных кубиков

    [HideInInspector]
    public int speed; // скорость падения фигур (1 - 5, 1 - пауза)
    [HideInInspector]
    public int score; //счет игрока
    public bool isLose = false; // переменная для обозначения поражения

	void Start ()//то,что проихойдет на старте сцены
    { 
        CubesPivot = new GameObject("CubesPivot"); //присваиваем объекту для хранения упавших кубиков пустой объект с именем CubesPivot;
        SceneCreatingFunc(sceneWidth, sceneHeight); // запускаем функцию создания сцены (указываем размеры сцены). В итоге получим заполненный массив _scene с учетом границ
        nextFigure = Random.Range(0, 7); // случайным образом выбираем номер следующей фигуры
        FigureSpawnFunc(nextFigure, 15, 10, mainMat, true); // запускаем функцию для спавна фигуры (номер фигуры, координаты ее положения, материал, является ли фигуры демонстрационной). Это создаст фигуру, которая выпадет в будущем
        FigureSpawnFunc(Random.Range(0,7),_scene.GetLength(0) / 2, sceneHeight, mainMat, false); // это создаст фигуру наверху игрового поля и она начнет падать вниз
        StartCoroutine("MoveDown"); // запускаем корутин для падения фигуры с разной скоростью.
    }

    void Update()// то, что выполняется каждый кадр
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) // если нажата стрелка вправо
        {
            if (isCanMove(actFigure, Vector3.right) == false) // если функция проверки возможности перемещения выдает false... (тут false означает свободный путь) (Родитель элементов, направление движения)
            {
                actFigure.position += Vector3.right; // перемещаем фигуру вправо на 1
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))// если нажата стрелка влево
        {
            if (isCanMove(actFigure, Vector3.left) == false)// если функция проверки возможности перемещения выдает false... (тут false означает свободный путь) (Родитель элементов, направление движения)
            {
                actFigure.position += Vector3.left;// перемещаем фигуру влево на 1
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) // если нажата стрелка вверх
        {
            Transform[] cubes = new Transform[actFigure.childCount]; // объявляем массив, в котором будут все кубики, из которых состоит падающая фигура
            for (int i = 0; i < cubes.Length; i++) // присваиваем кубики из падающей фигуры массиву
            {
                cubes[i] = actFigure.transform.GetChild(i);
            }
            if (isCanRot(actFigure, Vector3.forward) == false)// если функция проверки возможности поворота выдает false... (тут false означает свободный путь) (фигура для поворота, направление движения)
            {
                actFigure.localEulerAngles += Vector3.forward * 90; // поворачиваем фигуру
                for (int i = 0; i < cubes.Length; i++) // это просто поворачивает каждый элемент фигуры таким образом, чтобы он смотрел вверх (как лего)
                {
                    cubes[i].rotation = Quaternion.Euler(Vector3.right * -90);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Transform[] cubes = new Transform[actFigure.childCount]; // объявляем массив, в котором будут все кубики, из которых состоит падающая фигура
            for (int i = 0; i < cubes.Length; i++) // присваиваем кубики из падающей фигуры массиву
            {
                cubes[i] = actFigure.GetChild(i);
            }
            if (isCanRot(actFigure, Vector3.back) == false)// если функция проверки возможности поворота выдает false... (тут false означает свободный путь) (фигура для поворота, направление движения)
            {
                actFigure.localEulerAngles += Vector3.back * 90;// поворачиваем фигуру
                for (int i = 0; i < cubes.Length; i++)// это просто поворачивает каждый элемент фигуры таким образом, чтобы он смотрел вверх (как лего)
                {
                    cubes[i].rotation = Quaternion.Euler(Vector3.right * -90);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus)) // если нажат плюс, то...
        {
            speed++; // увеличиваем скорость
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus)) // если нажат минус, то...
        {
            speed--; // уменьшаем скорость
        }
        speed = Mathf.Clamp(speed, 1, 5);//скорость не может быть меньше 1 и больше 5
        if (speed < 2) // если скорость меньше 2, то...
        {
            Time.timeScale = 0; // игра ставится на паузу, фигуры перестают падать и все замирает
        }
        else { // иначе...
            Time.timeScale = 1; // игра идет в обычном режиме
        }

        if (CubesPivotChilds != CubesPivot.transform.childCount) // если изменилось количество упавших кубиков, то...
        {
            CubesPivotChilds = CubesPivot.transform.childCount; // присваиваем переменной, содержащей количество упавших кубиков новое число
            SceneChangerFunc(); //запускаем функцию обновления занятых полей в массиве
        }

        if (Input.GetKeyDown(KeyCode.Space))
        { // если нажат пробел, то...
            while (isCanMove(actFigure, Vector3.down) == false) //пока фигура может идти вниз, выполняем...
            {
                moveFunc(); //функцию движения вниз
            }
        }

        if (isCanMove(actFigure, Vector3.zero) == true) //если кубик при появлении или в какой-то другой момент накрыл собой занятую клетку, то...
        {
            isLose = true; //переменная поражения активируется, это включает другой скрипт, который перемещает камеру
            enabled = false; // сам скрипт отключается, чтобы не отлавливать нажатия на клавиатуру
        }
    }

    IEnumerator MoveDown() // корутин
    {
        yield return new WaitForSeconds(1f/(speed-1)); // ждем 1/(скорость-1) секунд и...
        if (isLose == false) // если игра еще не окончена...
        {
            StartCoroutine("MoveDown");//запускаем корутин на следующее движение вниз
            moveFunc(); // запускаем функцию падения кубика вниз на 1 клетку
        }    
    }

    void DeleteRowsFunc() { // функция удаления кубиков, которые заполнили всю ширину ряда и сдвиг верхних кубиков вниз

        //НАХОДИМ ТЕ КУБИКИ, КОТОРЫЕ ЗАПОЛНИЛИ СОБОЙ ВСЮ ШИРИНУ
        GameObject[] cubes = new GameObject[CubesPivot.transform.childCount];//объявляем массив для хранения кубиков, которые уже лежат внизу
        for (int i = 0; i < cubes.Length; i++) { // присваиваем все упавшие кубики массиву
            cubes[i] = CubesPivot.transform.GetChild(i).gameObject;
        }
        ArrayList rawsNumbers = new ArrayList(); //объявляем массив для хранения номера строки, в которой произошло заполнение
        for (int j = 1; j <= sceneHeight; j++) //перебираем строки от первой (нулевая - граница) до последней по высоте
        {
            int count = 0; // локальная переменная для хранения количества заполенных ячеек в каждой строке

            for (int k = 0; k < cubes.Length; k++) // перебираем все кубики, которые лежат на дне
            {
                if (Mathf.RoundToInt(cubes[k].transform.localPosition.y) == j) // если координа Y у кубика совпадает с номером строки, то это означает, что он лежит на ней и...
                {
                    count++; // к количеству заполненных ячеек в строке добавляем 1
                }
            }
            if (count == sceneWidth) // если количество заполненных ячеек равно ширине поля, то...
            {
                rawsNumbers.Add(j); // запоминаем в массиве номер строки
                score++; //прибавляем к счету 1
            }
        }
        myMedalScr.spawn(rawsNumbers.Count); //сбрасываем необходимое количество пуговиц

        //УДАЛЯЕМ КУБИКИ И СПУСКАЕМ ОСТАЛЬНЫЕ ВНИЗ
        for (int k = 0; k < cubes.Length; k++) // перебираем все кубики на дне
        {
            int numOfDown = 0; // объявляем локальную переменную, содержащую количество сдвигов кубика вниз на 1 клетку
            for (int i = 0; i < rawsNumbers.Count; i++) { //перебираем количество строк, в которых произошло заполнение
                if (Mathf.RoundToInt(cubes[k].transform.localPosition.y) == ((int)rawsNumbers[i])) // если  кубик лежит в этой строке, то...
                {
                    Destroy(cubes[k]); // удаляем кубик
                }
                if (Mathf.RoundToInt(cubes[k].transform.localPosition.y) > ((int)rawsNumbers[i]))//если кубик выше строки, то...
                {
                    numOfDown++; //добавляем к количеству сдвигов вниз 1
                }
            }
            cubes[k].transform.localPosition += Vector3.down * numOfDown; // сдвигаем кубик вниз на нужное количество строк
        }
    }

    void SceneChangerFunc() // функция обновления занятых полей в массиве
    {
        // 1 - свободно 0 - занято
        //ОСВОБОЖДАЕМ ВСЕ ПОЛЕ
        for (int x = 1; x <= sceneWidth; x++) // перебираем все поля по X
        {
            for (int y = 1; y <= sceneHeight; y++) // перебираем все поля по Y
            {
                _scene[x, y] = 1; //выбранная клетка равна 1
            }
        }

        //ЗАПОЛНЯЕМ ПОЛЕ УПАВШИМИ КУБИКАМИ 
        Vector3[] cubesPos = new Vector3[CubesPivotChilds]; // массив для хранения координат кубиков
        for (int i = 0; i < cubesPos.Length; i++) { // перебираем все кубики
            cubesPos[i] = CubesPivot.transform.GetChild(i).localPosition; // присваиваем координату кубика массиву, это можно не делать но ниже придется написать это if ((Mathf.RoundToInt(CubesPivot.transform.GetChild(i).localPosition.x) == x) && (Mathf.RoundToInt(CubesPivot.transform.GetChild(i).localPosition.y) == y))
            for (int x = 1; x <= sceneWidth; x++) // перебираем поля по X
            {
                for (int y = 1; y <= sceneHeight; y++) // перебираем поля по Y
                {
                    if ((Mathf.RoundToInt(cubesPos[i].x) == x) && (Mathf.RoundToInt(cubesPos[i].y) == y)) //если положение кубика совпадает с координатой клетки, то...
                    {
                        _scene[x, y] = 0; // эта клетка занята
                    }
                }
            }
        }
    }

    void FigureSpawnFunc(int figNum, float figureXPos, float figureYPos, Material mat, bool isNextFig) //функция спавна фигуры (номер фигурыб положение по X, положение по Y, материал для кубиков фигуры, является ли фигура демонстрационной)
    {
        GameObject figure = new GameObject("FigurePivot"); //создаем пустой объект, который будет родителем для кубиков (элементов) фигуры
        if (figNum == 0) { // создание S
            figure.transform.position = new Vector3(figureXPos, figureYPos, 0); //распологаем центр фигуры наверху
            CubeCreatingFunc(0, 0 - 1, Color.magenta, figure.transform); //вызываем функцию создания кубика (координаты (локальные), цвет, родитель)
            CubeCreatingFunc(0 - 1, 0 - 1, Color.magenta, figure.transform);
            CubeCreatingFunc(0, 0, Color.magenta, figure.transform);
            CubeCreatingFunc(0 + 1, 0, Color.magenta, figure.transform);
        }
        else if (figNum == 1) // создание I
        {
            figure.transform.position = new Vector3(figureXPos-0.5f, figureYPos+0.5f, 0);
            CubeCreatingFunc(0-0.5f, 0 + 1.5f, Color.blue, figure.transform);
            CubeCreatingFunc(0-0.5f, 0 + 0.5f, Color.blue, figure.transform);
            CubeCreatingFunc(0-0.5f, 0-0.5f, Color.blue, figure.transform);
            CubeCreatingFunc(0-0.5f, 0 - 1.5f, Color.blue, figure.transform);
        }
        else if(figNum == 2) // создание O
        {
            figure.transform.position = new Vector3(figureXPos-0.5f, figureYPos-0.5f, 0);
            CubeCreatingFunc(0-0.5f, 0 - 0.5f, Color.green, figure.transform);
            CubeCreatingFunc(0-0.5f, 0 + 0.5f, Color.green, figure.transform);
            CubeCreatingFunc(0+0.5f, 0 - 0.5f, Color.green, figure.transform);
            CubeCreatingFunc(0+0.5f, 0 + 0.5f, Color.green, figure.transform);
        }
        else if (figNum == 3) // создание T
        {
            figure.transform.position = new Vector3(figureXPos, figureYPos-1, 0);
            CubeCreatingFunc(0, 0, Color.red, figure.transform);
            CubeCreatingFunc(0 - 1, 0, Color.red, figure.transform);
            CubeCreatingFunc(0 + 1, 0, Color.red, figure.transform);
            CubeCreatingFunc(0, 0+1, Color.red, figure.transform);
        }
        else if (figNum == 4) // создание J
        {
            figure.transform.position = new Vector3(figureXPos, figureYPos, 0);
            CubeCreatingFunc(0, 0-1, Color.yellow, figure.transform);
            CubeCreatingFunc(0 - 1, 0-1, Color.yellow, figure.transform);
            CubeCreatingFunc(0, 0, Color.yellow, figure.transform);
            CubeCreatingFunc(0, 0 + 1, Color.yellow, figure.transform);
        }
        else if (figNum == 5) // создание L
        {
            figure.transform.position = new Vector3(figureXPos, figureYPos, 0);
            CubeCreatingFunc(0, 0 - 1, Color.grey, figure.transform);
            CubeCreatingFunc(0 + 1, 0 - 1, Color.grey, figure.transform);
            CubeCreatingFunc(0, 0, Color.grey, figure.transform);
            CubeCreatingFunc(0, 0 + 1, Color.grey, figure.transform);
        }
        else // создание Z
        {
            figure.transform.position = new Vector3(figureXPos, figureYPos, 0);
            CubeCreatingFunc(0, 0 - 1, Color.cyan, figure.transform);
            CubeCreatingFunc(0 + 1, 0 - 1, Color.cyan, figure.transform);
            CubeCreatingFunc(0, 0, Color.cyan, figure.transform);
            CubeCreatingFunc(0 - 1, 0, Color.cyan, figure.transform);
        }

        if (isNextFig == false) //если фигура не для демонстрации, то...
        {
            actFigure = figure.transform;
        }
        else { //иначе, если фигура демонстрационная, то...
            figure.AddComponent<Rigidbody>(); //придаем физику фигуре
            demoFigure = figure.transform;
        }
    }

    void moveFunc() { //функция падения фигуры вниз
        Transform[] cubes = new Transform[actFigure.transform.childCount];//локальная переменная для хранения кубиков (элементов фигуры)
        for (int i = 0; i < cubes.Length; i++) {//перебираем каждый кубик
            cubes[i] = actFigure.transform.GetChild(i);//присваиваем каждый кубик массиву
        }
        if (isCanMove(actFigure.transform, Vector3.down) == true) //если кубик НЕ может двигаться вниз, то...
        {
            for (int i = 0; i < cubes.Length; i++) //перебираем каждый кубик в фигуре
            {
                cubes[i].SetParent(CubesPivot.transform); //делаем кубик дочерним к элементу, содержащему все кубики, которые лежат внизу
            }
            Destroy(actFigure.gameObject); //уничтожаем элемент фигуру (пустой объект)
            DeleteRowsFunc(); //вызываем функцию удаления кубиков, которые заполнили ряд
            SceneChangerFunc();//вызываем функцию для обновления занятых и пустых мест на поле
            //Destroy(GameObject.FindGameObjectWithTag("nextFigure"));//уничтожаем демонстрационную фигуру
            Destroy(demoFigure.gameObject);
            FigureSpawnFunc(nextFigure, _scene.GetLength(0) / 2, sceneHeight, mainMat, false);//спавним новую фигуру сверху
            nextFigure = Random.Range(0, 7);//случайног выбираем номер следующей фигуры
            FigureSpawnFunc(nextFigure, 15, 10, mainMat, true);//спавним демонстрационную фигуру
        }
        else { //иначе, если фигура может двигаться вниз, то...
            actFigure.transform.position += Vector3.down; //двигаем фигуру вниз на 1
        }
    }

    void SceneCreatingFunc(int width, int height) // функция создания поля длля игры
    {
        _scene = new int[width + 2, height + 4]; //создаем двумерный массив (+2 - учет границ по бокам, +4 - учет границы снизу и высоты фигуры)
        for (int x = 0; x < _scene.GetLength(0); x++) //перебираем все поля по X (с учетом границ)
        {
            for (int y = 0; y < _scene.GetLength(1); y++)//перебираем все поля по Y (с учетом границ)
            {
                if ((x > 0) && (x < _scene.GetLength(0) - 1) && (y != 0))//если клетка по X между 0 границей и по Y не ноль, то...
                {
                    _scene[x, y] = 1;//клетка равна 1 (значит свободна)
                }
                else { //иначе...
                    _scene[x, y] = 0; //клетка равно 0 (занята)
                }
            }
        }
    }

    void CubeCreatingFunc(float x, float y, Color color, Transform parent)//функция создания кубика (координаты, цвет, родитель)
    {
        GameObject cube = Instantiate(CubePref);//создаем новый объект из префаба
        cube.transform.parent = parent;//делаем его дочерним радителю
        cube.transform.localPosition = new Vector3(x, y, 0);//настраиваем координты
        cube.GetComponent<Renderer>().material.color = color;//даем цвет
    }
    bool isCanMove(Transform figure, Vector3 direction) //функция проверки на передвижение (кубики (массив), направление)
    {
        Vector3[] cubes = new Vector3[figure.childCount];//локальная переменная для хранения положения кубиков - элементов массива
        for (int i = 0; i < cubes.Length; i++)// перебираем каждый кубик
        { 
            cubes[i] = figure.GetChild(i).position;
            //если в том направлении, куда направится кубик на поле сцены стоит 0, то вернуть True, т.е. идти нельзя
            if (_scene[Mathf.RoundToInt(cubes[i].x + direction.x), Mathf.RoundToInt(cubes[i].y + direction.y)] == 0) return true;
        }

        return false; //вернуть False, т.е. идти можно
    }
    bool isCanRot(Transform pivot, Vector3 direction)//функция проверки фигуры на поворот (фигура, направление)
    {
        pivot.localEulerAngles += direction * 90; // поворачиваем фигуру в нужном направлении на 90 градусов
        Vector3[] cubes = new Vector3[pivot.childCount]; // объявляем массив для хранения положения кубиков - элементов фигуры
        for (int i = 0; i < cubes.Length; i++)//перебираем кубики
        {
            cubes[i] = pivot.GetChild(i).position; //присваиваем кубик массиву
            if (_scene[Mathf.RoundToInt(cubes[i].x), Mathf.RoundToInt(cubes[i].y)] == 0) // если после поворота кубик находится на клетке сцены, которая занята, то...
            {
                pivot.localEulerAngles -= direction * 90; //поврачиваем фигуру обратно
                return true; //вернуть True, т.е.поврот запрещен
            }
        }
        pivot.localEulerAngles -= direction * 90; // поворачиваем фигуру обратно
        return false; //возвращаем false, т.е. поворот разрешен
    }
}
