using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSmall : Asteroid
{

    public override void Destroy(int who = 0)
    {
        GameObject go = GameObject.Find("GameController");
        GameControl gControl = go.GetComponent<GameControl>();

        if (who == 0)
            gControl.gameScene.AssScore(15);

        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //InitializeRandomAsteroid();

        //int id = Random.Range(1, 3);
        //Vector3 screenPos;
        //if (id == 1)
        //    screenPos = new Vector3(0, Random.Range(0f, Screen.height), 1);
        //else
        //    screenPos = new Vector3(Random.Range(0f, Screen.width), 0, 1);
        //position = Camera.main.ScreenToWorldPoint(screenPos);
        //force = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0);
        //rotation = Quaternion.Euler(0, 0, Random.Range(0f, 359f));
        //forceRotation = Random.Range(-20f, 20f);
        //size = 1;
        //idType = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
