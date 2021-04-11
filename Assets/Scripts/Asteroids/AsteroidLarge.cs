using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidLarge : Asteroid
{


    public override void Destroy(int who = 0)
    {
        GameObject go = GameObject.Find("GameController");
        GameControl gControl = go.GetComponent<GameControl>();
        if (who == 0)
            gControl.gameScene.AssScore(15);

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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
