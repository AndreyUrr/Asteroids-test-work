using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeControl : MonoBehaviour
{

    

    public float liveTime = 2f;
    public float speed = 4f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("коллизия активирована");
        //GameObject obj = collision.gameObject;
        Asteroid aster = (Asteroid)collision.GetComponent<Asteroid>();
        Debug.Log("коллизия сработала. объект " + aster);
        if (aster != null)
        {
            aster.Destroy();
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("поймалось что-то страшное. Это не астероид!");
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
