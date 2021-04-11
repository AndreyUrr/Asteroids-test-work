using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeControl : MonoBehaviour
{

    

    public float liveTime = 2f;
    public float speed = 4f;
    public int who = 0; //0 - spaceShip, 1 - alien


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("коллизия активирована");
        //GameObject obj = collision.gameObject;
        Asteroid aster = (Asteroid)collision.GetComponent<Asteroid>();
        Debug.Log("коллизия сработала. объект " + aster);
        if (aster != null)
        {
            aster.Destroy(who);
            Destroy(this.gameObject);
        }
        else
        {
            Alien alien = collision.GetComponent<Alien>();
            if (alien != null && who == 0)
            {
                alien.Destroy(who);
                Destroy(this.gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        if (liveTime > 0)
        {
            this.gameObject.transform.position += gameObject.transform.up * speed * Time.deltaTime;
            liveTime -= 1 * Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
