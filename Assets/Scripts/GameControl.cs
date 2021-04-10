using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class GameControl : MonoBehaviour
{

    public Canvas mainMenu;
    public Canvas gameMenu;
    public GameObject spaceShip;
    public GameObject sceneObjects;


    public GameObject pAsterL1;
    public GameObject pAsterL2;
    public GameObject pAsterM1;
    public GameObject pAsterM2;
    public GameObject pAsterS1;
    public GameObject pAsterS2;
    public GameObject pStrike;
    public UnityEngine.UI.Text labelToch;
    public UnityEngine.UI.Text labelToch2;

    public StrikeTrigger strikeTrg;
    public StrikeTrigger toLeft;
    public StrikeTrigger toRight;
    public StrikeTrigger toRun;


    bool isGaming = false;
    bool isGameOver = false;
    GameScene gameScene;
    //public Resolution[] resolutions = Screen.resolutions;
    //public float sceneWidth = Screen.width;
    //public float screenHeight = Screen.height;

    public Vector3 shipPos = new Vector3();

    public float strikeDelay = 0.15f;
    float timeToStrike = 0;

    public Vector3 shipSpeedVector = new Vector3(0, 0, 0);
    public float aForceEngine = 1;

    public void StartNewGame()
    {
        mainMenu.enabled = false;
        gameMenu.enabled = true;
        gameScene = new GameScene();
        isGaming = true;
        isGameOver = false;

        for (int i = 0; i < 3; i++)
        {
            //gameScene.GenerateAsteroid();
            GenerateAsteroidInScene();
        }

        spaceShip.transform.localScale = new Vector3(0.1f, 0.1f, 0);
    }
    public void GenerateAsteroidInScene(Vector3 position, Quaternion rotation, Vector3 force)
    {
        GameObject sceneAster;
        int asterId = Random.Range(1, 3);
        if (asterId == 1)
            sceneAster = Instantiate(pAsterS1, position, rotation) as GameObject;
        else
            sceneAster = Instantiate(pAsterS2, position, rotation) as GameObject;

        sceneAster.transform.SetParent(sceneObjects.transform);
    }
    public void GenerateAsteroidInScene()
    {

        int asterId = Random.Range(1, 3);
        int AsterType = Random.Range(1, 4);
        //int typeAster = aster.idType;
        GameObject sceneAster;

        switch (AsterType)
        {
            case 1:
                if (asterId == 1)
                    sceneAster = Instantiate(pAsterS1) as GameObject;
                else
                    sceneAster = Instantiate(pAsterS2) as GameObject;
                break;
            case 2:
                if (asterId == 1)
                    sceneAster = Instantiate(pAsterM1) as GameObject;
                else
                    sceneAster = Instantiate(pAsterM2) as GameObject;
                break;
            case 3:
                if (asterId == 1)
                    sceneAster = Instantiate(pAsterL1) as GameObject;
                else
                    sceneAster = Instantiate(pAsterL2) as GameObject;
                break;
            default:
                return;
        }
        //aster.objectInScene = sceneAster;
        sceneAster.transform.SetParent(sceneObjects.transform);
        Asteroid a = sceneAster.GetComponent<Asteroid>();
        if (a != null)
        {
            a.InitializeRandomAsteroid();
        }
        Debug.Log("астероид создан");
    }

    public void RotateGameObject(GameObject obj, float rotation)
    {
        obj.transform.Rotate(0, 0, rotation*Time.deltaTime*3);
    }

    #region oldVersion
    /// <summary>
    /// Устарело, не использовать. Использовать метод юнита
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="force"></param>
    //public void MoveGameObject(GameObject obj, Vector3 force)
    //{
    //    //obj.transform.position.z = 0;
    //    obj.transform.position += force * 1.2f * Time.deltaTime;
    //    Vector3 worldPos = Camera.main.WorldToScreenPoint(obj.transform.position);
    //    worldPos.z = 1;
    //    float allowance = 40f;
    //    //Camera.main.ScreenToWorldPoint();
    //    if (worldPos.x > Screen.width + allowance)
    //    {
    //        obj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(worldPos.x - Screen.width, worldPos.y, worldPos.z));
    //    }
    //    if (worldPos.x < 0 - allowance)
    //    {
    //        obj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(worldPos.x + Screen.width, worldPos.y, worldPos.z));
    //    }
    //    if (worldPos.y > Screen.height + allowance)
    //    {
    //        obj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(worldPos.x, worldPos.y - Screen.height, worldPos.z));
    //    }
    //    if (worldPos.y < 0 - allowance)
    //    {
    //        obj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(worldPos.x, worldPos.y + Screen.height, worldPos.z));
    //    }

    //    //ShipPos = spaceShip.transform.position;
    //}
    #endregion
    public void InputRun()
    {
        aForceEngine += 0.2f;
        shipSpeedVector += spaceShip.transform.up * 1.1f * aForceEngine * Time.deltaTime;
        //shipSpeedVector += spaceShip.transform.
    }
    public void InputLeft()
    {
        spaceShip.transform.rotation = Quaternion.Euler(0f, 0f, (spaceShip.transform.rotation.eulerAngles.z + 130*Time.deltaTime));
    }
    public void InputRight()
    {
        spaceShip.transform.rotation = Quaternion.Euler(0f, 0f, (spaceShip.transform.rotation.eulerAngles.z - 130*Time.deltaTime));
    }

    public void InputStrike()
    {
        if (timeToStrike <= Time.time)
        {
            Strike();
            timeToStrike = Time.time + strikeDelay;
            //Debug.Log("Время: " + Time.time + ", блокировка до времени: " + timeToStrike);
        }
    }
    public void Strike()
    {
        GameObject strike = Instantiate(pStrike, spaceShip.transform.position + spaceShip.transform.up * 0.2f, spaceShip.transform.rotation) as GameObject;
        strike.transform.SetParent(sceneObjects.transform);
    }
    public void InputExitToMenu()
    {
        gameMenu.enabled = false;
        mainMenu.enabled = true;
        isGaming = false;
        isGameOver = false;
    }


    public void Ouit()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameMenu.enabled = false;
        mainMenu.enabled = true;
    }

    
    // Update is called once per frame
    void Update()
    {

        if (isGaming)   //main Gaming loop
        {

            ShipControl sc = spaceShip.GetComponent<ShipControl>();

            int ll1 = sc.Lives;
            sc.Lives = -2;

            #region input and moving

            if (strikeTrg.IsPressed)
            {
                InputStrike();
                labelToch2.text = "pressed";
            }
            else
            {
                labelToch2.text = "not";
            }

            if (Input.touchCount > 0)
            {
                

                //Touch touch = Input.GetTouch(0);
                //labelToch.text = touch.position.x +", "+touch.position.y;
                ////labelToch2.text = Screen.width + ", " + Screen.height;
                //Vector2 pos = touch.position;
                //if (pos.y < Screen.height - Screen.height/4)
                //{

                //    //InputStrike();
                //}
            }

            if (Input.GetMouseButton(0))
            {
                //InputStrike();
            }

            if (Input.GetKey("d") || toRight.IsPressed)
            {
                InputRight();
            }
            if (Input.GetKey("a") || toLeft.IsPressed)
            {
                InputLeft();
            }
            if (Input.GetKeyDown("w") || !toRun.IsPressed)
            {
                aForceEngine = 1;
            }
            if (Input.GetKey("w") || toRun.IsPressed)
            {
                InputRun();

            }
            #endregion

            //MoveGameObject(spaceShip, shipSpeedVector);
            ShipControl ship = spaceShip.GetComponent<ShipControl>();
            ship.Move(shipSpeedVector);


            #region передвижение объектов новое

            //var test = sceneObjects.GetComponentInChildren<Transform>();
            foreach (Transform t in sceneObjects.GetComponentInChildren<Transform>())
            {
                Asteroid aster = t.gameObject.GetComponent<Asteroid>();
                if (aster != null)
                {
                    aster.Move();
                    aster.Rotate();
                }
                //AsteroidLarge asterL = t.gameObject.GetComponent<AsteroidLarge>();
                //if (asterL != null)
                //{
                //    asterL.Move();
                //}
                //AsteroidMedium asterM = t.gameObject.GetComponent<AsteroidMedium>();
                //if (asterM != null)
                //{
                //    asterM.Move();
                //}
                //AsteroidSmall asterS = t.gameObject.GetComponent<AsteroidSmall>();
                //if (asterS != null)
                //{
                //    asterS.Move();
                //}
                //MoveGameObject(obj, )
            }

            #endregion


            //foreach (var aster in gameScene.asteroids)
            //{
            //    MoveGameObject(aster.objectInScene, aster.force);
            //    RotateGameObject(aster.objectInScene, aster.forceRotation);
            //}

            if (Time.time > gameScene.timeNextGenAster)
            {
                gameScene.SetNextTimeGenAster();
                if (true)
                {
                    //gameScene.GenerateAsteroid();
                    GenerateAsteroidInScene();
                }
            }

            #region old moving
            //spaceShip.transform.position += shipSpeedVector * 0.1f;
            //Vector3 worldPos = Camera.main.WorldToScreenPoint(spaceShip.transform.position);
            ////Camera.main.ScreenToWorldPoint();
            //if (worldPos.x > Screen.width)
            //{
            //    spaceShip.transform.position = Camera.main.ScreenToWorldPoint( new Vector3(worldPos.x - Screen.width, worldPos.y, worldPos.z));
            //}
            //if (worldPos.x < 0)
            //{
            //    spaceShip.transform.position = Camera.main.ScreenToWorldPoint( new Vector3(worldPos.x + Screen.width, worldPos.y, worldPos.z));
            //}
            //if (worldPos.y > Screen.height)
            //{
            //    spaceShip.transform.position = Camera.main.ScreenToWorldPoint( new Vector3(worldPos.x, worldPos.y - Screen.height, worldPos.z));
            //}
            //if (worldPos.y < 0)
            //{
            //    spaceShip.transform.position = Camera.main.ScreenToWorldPoint( new Vector3(worldPos.x, worldPos.y + Screen.height, worldPos.z));
            //}

            //ShipPos = spaceShip.transform.position;
            #endregion
        }

        if (isGameOver)
        {
            
        }
    }
}

public class GameScene
{
    public int score { get; set;}
    

    public float timeStart { get; set; }
    public float timeNextGenAster { get; private set; }
    public float timeIntervalGenAster { get; private set; }

    //public List<AsterOld> asteroids;

    //public Alien alien;
    public GameScene()
    {
        timeIntervalGenAster = 5f;
        score = 0;
        timeStart = Time.time;
        timeNextGenAster = timeStart + timeIntervalGenAster;
        //alien = new Alien();

    }

    public void SetNextTimeGenAster()
    {
        timeNextGenAster += timeIntervalGenAster;
    }


    /// <summary>
    /// Generate random type of asteroid
    /// </summary>
    //public void GenerateAsteroid()
    //{
    //    int id = Random.Range(1, 4);
    //    switch (id)
    //    {
    //        case 1:
    //            asteroids.Add(new AsteroidLargeOld());
    //            break;
    //        case 2:
    //            asteroids.Add(new AsteroidMediumOld());
    //            break;
    //        case 3:
    //            asteroids.Add(new AsteroidSmallOld());
    //            break;
    //    }
    //
    //}
    public void MoveAsteroids()
    {
        
    }

    /// <summary>
    /// Clear asteroids out of game zone
    /// </summary>
    public void ClearAsteroids()
    {
        
    }


}



//public class AsterOld
//{
//    public Vector3 position;
//    public Vector3 force;   //force direction
//    public Quaternion rotation;
//    public float forceRotation;
//    public float size;    //radius of asteroid
//    public int idType = 0;
//    public GameObject objectInScene;
//    public AsterOld()
//    {
//        int id = Random.Range(1, 3);
//        Vector3 screenPos;
//        if (id == 1)
//            screenPos = new Vector3(0, Random.Range(0f, Screen.height), 1);
//        else
//            screenPos = new Vector3(Random.Range(0f, Screen.width), 0, 1);
//        position = Camera.main.ScreenToWorldPoint(screenPos);
//        force = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0);
//        rotation = Quaternion.Euler(0, 0, Random.Range(0f, 359f));
//        forceRotation = Random.Range(-20f, 20f);
//        size = 1;
//    }
//    public void Move()
//    {

//    }

//    public virtual void Destroy()
//    {
//        Debug.Log("Destroy: в астероид попали!");
//    }
//}

//public class AsteroidLargeOld : AsterOld
//{
//    public AsteroidLargeOld() : base()
//    {
//        size = 1;
//        idType = 3;
//    }
//}
//public class AsteroidMediumOld : AsterOld
//{
//    public AsteroidMediumOld() : base()
//    {
//        size = 0.5f;
//        idType = 2;
//    }
//}
//public class AsteroidSmallOld : AsterOld
//{
//    public AsteroidSmallOld() : base()
//    {
//        size = 0.25f;
//        idType = 1;
//    }
//}

//public class Alien
//{
    
//}