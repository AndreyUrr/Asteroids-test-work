using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameControl : MonoBehaviour
{

    public Canvas mainMenu;
    public Canvas gameMenu;
    private GameObject spaceShip;
    public GameObject sceneObjects;


    public GameObject canvasGamingPart;
    public GameObject canvasGameOverPart;
    public GameObject canvasMainMenuPart;

    public GameObject pSpaceShip;
    public GameObject pAsterL1;
    public GameObject pAsterL2;
    public GameObject pAsterM1;
    public GameObject pAsterM2;
    public GameObject pAsterS1;
    public GameObject pAsterS2;
    public GameObject pStrike;
    public GameObject pAlien;
    public UnityEngine.UI.Text labelScore;

    public GameObject live1;
    public GameObject live2;
    public GameObject live3;


    public StrikeTrigger strikeTrg;
    public StrikeTrigger toLeft;
    public StrikeTrigger toRight;
    public StrikeTrigger toRun;


    bool isGaming = false;
    bool isGameOver = false;
    public bool IsGameOver { set { isGameOver = value; } get { return isGameOver; } }
    public GameScene gameScene;
    //public Resolution[] resolutions = Screen.resolutions;
    //public float sceneWidth = Screen.width;
    //public float screenHeight = Screen.height;

    public Vector3 shipPos = new Vector3();

    public float strikeDelay = 0.15f;
    float timeToStrike = 0;

    public Vector3 shipSpeedVector = new Vector3(0, 0, 0);
    public float aForceEngine = 1;


    private void ClearGameZone()
    {
        shipSpeedVector = new Vector3(0, 0, 0);

        if (spaceShip != null)
        {
            Destroy(spaceShip.gameObject);
        }

        foreach (Transform t in sceneObjects.GetComponentInChildren<Transform>())
        {
            Asteroid aster = t.gameObject.GetComponent<Asteroid>();
            if (aster != null)
            {
                Destroy(aster.gameObject);
            }
            else
            {
                StrikeControl sControl = t.gameObject.GetComponent<StrikeControl>();
                if (sControl != null)
                {
                    Destroy(sControl.gameObject);
                }
                else
                {
                    Alien alien = t.gameObject.GetComponent<Alien>();
                    if (alien != null)
                    {
                        Destroy(alien.gameObject);
                    }
                }
            }

        }
    }

    public void StartNewGame()
    {
        ClearGameZone();

        mainMenu.enabled = false;
        canvasMainMenuPart.SetActive(false);
        gameMenu.enabled = true;
        gameScene = new GameScene();
        isGaming = true;
        isGameOver = false;
        canvasGameOverPart.SetActive(false);
        canvasGamingPart.SetActive(true);
        live1.SetActive(true);
        live2.SetActive(true);
        live3.SetActive(true);

        


        spaceShip = Instantiate(pSpaceShip, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));


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

    public void LiveControl(int lives)
    {
        if (lives < 3)
        {
            live3.SetActive(false);
        }
        else
        {
            live3.SetActive(true);
        }
        if (lives < 2)
        {
            live2.SetActive(false);
        }
        else
        {
            live2.SetActive(true);
        }
        if (lives < 1)
        {
            live1.SetActive(false);
        }
        else
        {
            live1.SetActive(true);
        }
    }

    public void GenerateAlienInScene()
    {
        GameObject alien = Instantiate(pAlien) as GameObject;
        alien.transform.SetParent(sceneObjects.transform);
        Alien a = alien.GetComponent<Alien>();
        if (a != null)
        {
            a.spaceShip = spaceShip;
        }


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
        if (spaceShip != null)
        {
            Destroy(spaceShip.gameObject);
        }
        ClearGameZone();
        gameMenu.enabled = false;
        mainMenu.enabled = true;
        isGaming = false;
        isGameOver = false;
        live1.SetActive(true);
        live2.SetActive(true);
        live3.SetActive(true);
        canvasGameOverPart.SetActive(false);
        canvasGamingPart.SetActive(false);
        canvasMainMenuPart.SetActive(true);
    }


    public void Ouit()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        
        isGameOver = true;
        isGaming = true;
        canvasGameOverPart.SetActive(true);
        canvasGamingPart.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameMenu.enabled = false;
        mainMenu.enabled = true;
        canvasGamingPart.SetActive(false);
        
    }

    
    // Update is called once per frame
    void Update()
    {

        if (isGaming && !isGameOver)   //main Gaming loop
        {

            labelScore.text = gameScene.Score.ToString();

            ShipControl sc = spaceShip.GetComponent<ShipControl>();


            #region input and moving

            if (strikeTrg.IsPressed)
            {
                InputStrike();
            }


            if (Input.touchCount > 0)
            {
                
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
                else
                {
                    Alien alien = t.gameObject.GetComponent<Alien>();
                    if (alien != null)
                    {
                        alien.Move();
                    }
                }

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
            if (Time.time > gameScene.timeNextGenALien)
            {
                gameScene.SetNextTimeGenALien();
                GenerateAlienInScene();
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
    private int score { get; set;}
    public int Score { get { return score; } }

    public float timeStart { get; set; }
    public float timeNextGenAster { get; private set; }
    public float timeNextGenALien { get; private set; }
    public float timeIntervalGenAster { get; private set; }
    public float timeIntervalGenAlien { get; private set; }
    //public List<AsterOld> asteroids;

    //public Alien alien;
    public GameScene()
    {
        timeIntervalGenAster = 5f;
        timeIntervalGenAlien = 3f;
        score = 0;
        timeStart = Time.time;
        timeNextGenAster = timeStart + timeIntervalGenAster;
        timeNextGenALien = timeStart + timeIntervalGenAlien;
        //alien = new Alien();

    }

    public void AssScore(int value)
    {
        score += value;

    }
    public void SetNextTimeGenALien()
    {
        timeNextGenALien += timeIntervalGenAlien;
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