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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
