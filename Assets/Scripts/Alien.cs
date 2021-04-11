using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : Unit
{
    private int directionType = 0; // -1, 0, 1

    public GameObject spaceShip;

    private GameControl gControl;

    private float timeIntChangeDirection = 3f;  //interval
    private float timeNextChangeDirection;

    private float timeIntervalStrike = 5f;  //5
    private float timeNextStrike;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Asteroid aster = collision.gameObject.GetComponent<Asteroid>();
        if (aster != null)
        {
            Destroy(aster.gameObject);
            Destroy(this.gameObject);
        }
    }

    public override void Destroy(int who = 0)
    {
        if (who == 0)
        {
            gControl.gameScene.AssScore(50);
            Destroy(this.gameObject);
        }
    }
    private void ChangeDirection()
    {
        if (directionType == 0)
        {
            int rand = Random.Range(1, 3);
            if (rand == 1)
            {
                directionMove = directionMove + new Vector3(0, 0.5f, 0);
                directionType = 1;
            }
            else
            {
                directionMove = directionMove + new Vector3(0, -0.5f, 0);
                directionType = -1;
            }
        }
        else if (directionType == -1)
        {
            directionMove = directionMove + new Vector3(0, 0.5f, 0);
            directionType = 0;
        }
        else if (directionType == 1)
        {
            directionMove = directionMove + new Vector3(0,-0.5f, 0);
            directionType = 0;
        }
    }
    public void Move()
    {
        Move(directionMove);
    }
    protected override void SetStartRandomPosition()
    {
        Vector3 screenPos = new Vector3(-allowance, Random.Range(0f, Screen.height), 1);
        this.gameObject.transform.position = Camera.main.ScreenToWorldPoint(screenPos);
    }
    protected override void SetRandomDirection()
    {
        int rand = Random.Range(1, 3);
        if (rand == 1)
        {
            directionMove = new Vector3(Random.Range(0.3f, 0.6f), 0f, 0f);
        }
        else
        {
            directionMove = new Vector3(Random.Range(-0.3f, -0.6f), 0f, 0f);
        }
    }

    public void Strike()
    {
        Vector3 direction = new Vector3(spaceShip.transform.position.x - this.gameObject.transform.position.x,
                                        spaceShip.transform.position.y - this.gameObject.transform.position.y,
                                        0);
        //direction.Normalize();
        Quaternion rotation = Quaternion.LookRotation(new Vector3(0, 0, -1), direction);
        GameObject strike = Instantiate(gControl.pStrike, this.gameObject.transform.position, rotation) as GameObject;
        strike.transform.SetParent(gControl.sceneObjects.transform);
        strike.GetComponent<StrikeControl>().who = 1;
    }
    public void InitializeAlien()
    {
        SetStartRandomPosition();
        SetRandomDirection();
        timeNextChangeDirection = timeIntChangeDirection + Time.time;
        timeNextStrike = timeIntervalStrike + Time.time;

    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeAlien();

        GameObject go = GameObject.Find("GameController");
        gControl = go.GetComponent<GameControl>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time > timeNextChangeDirection)
        {
            ChangeDirection();
            timeNextChangeDirection += timeIntChangeDirection;
        }

        if (Time.time > timeNextStrike && !gControl.IsGameOver)
        {
            timeNextStrike += timeIntervalStrike;
            Strike();
        }
    }
}
