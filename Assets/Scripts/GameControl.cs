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

    public void InputRun()
    {
        aForceEngine += 0.8f * Time.deltaTime;
        if (shipSpeedVector.magnitude < 15)
        {
            shipSpeedVector += spaceShip.transform.up * 1.4f * aForceEngine * Time.deltaTime;
        }
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

            ShipControl ship = spaceShip.GetComponent<ShipControl>();
            ship.Move(shipSpeedVector);


            #region передвижение объектов

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


            if (Time.time > gameScene.timeNextGenAster)
            {
                gameScene.SetNextTimeGenAster();
                if (true)
                {
                    GenerateAsteroidInScene();
                }
            }
            if (Time.time > gameScene.timeNextGenALien)
            {
                gameScene.SetNextTimeGenALien();
                GenerateAlienInScene();
            }

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


    public GameScene()
    {
        timeIntervalGenAster = 5f;
        timeIntervalGenAlien = 15f;  //15
        score = 0;
        timeStart = Time.time;
        timeNextGenAster = timeStart + timeIntervalGenAster;
        timeNextGenALien = timeStart + timeIntervalGenAlien;

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

}
