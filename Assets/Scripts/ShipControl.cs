using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : Unit
{

    private GameControl gControl;
    [SerializeField]
    private int lives = 3;
    public int Lives { get { return lives; } set { lives = value; } }

    public int GetLives()
    {
        return lives;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int who = 1;
        StrikeControl obj = collision.GetComponent<StrikeControl>();
        if (obj != null)
        {
            who = obj.who;
        }
        if (who == 1)
        {
            if (lives > 0)
            {
                lives--;
                gControl.LiveControl(lives);
                Destroy(collision.gameObject);
            }
            else
            {
                gControl.GameOver();
                Destroy(this.gameObject);
                Destroy(collision.gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("GameController");
        gControl = go.GetComponent<GameControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
}
