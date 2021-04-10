using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidLarge : Asteroid
{


    public override void Destroy()
    {
        GameObject go = GameObject.Find("GameController");
        GameControl gControl = go.GetComponent<GameControl>();
        if (gControl != null)
        {
            GameObject gameScene = this.gameObject.transform.parent.gameObject;
            GameObject aster1 = Instantiate(gControl.pAsterM1, this.position, this.rotation) as GameObject;
            GameObject aster2 = Instantiate(gControl.pAsterM2, this.position, this.rotation) as GameObject;

            Asteroid a1 = aster1.GetComponent<Asteroid>();
            Asteroid a2 = aster2.GetComponent<Asteroid>();
            if (a1 != null && a2 != null)
            {
                Vector3 dir = this.directionMove;
                a1.InitializeAsteroid(this.gameObject.transform.position, this.directionMove + new Vector3(-dir.y, dir.x, dir.z) * Random.Range(lowValDeviation, hightValDeviation));
                a2.InitializeAsteroid(this.gameObject.transform.position, this.directionMove - new Vector3(-dir.y, dir.x, dir.z) * Random.Range(lowValDeviation, hightValDeviation));
            }

            aster1.transform.SetParent(gameScene.transform);
            aster2.transform.SetParent(gameScene.transform);
            Destroy(this.gameObject);
        }
        
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
        //idType = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
