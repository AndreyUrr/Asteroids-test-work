using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : Unit
{
    private int lives = 3;
    public int Lives { get { return lives; } set { lives = value; } }
    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("Collision Enter");
    //}

    public int GetLives()
    {
        return lives;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Ship: Collision entered.");
        if (lives > 0)
        {
            lives--;
        }
        else
        {
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
