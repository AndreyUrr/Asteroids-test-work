using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Unit
{
    public Vector3 position;    //start point of position
    public Vector3 force;   //force direction
    public Quaternion rotation; //start point of rotation
    //public float forceRotation;
    public float size;    //radius of asteroid
    public int idType = 0;  //type of asteroid (1 - small, 2 - medium, 3 - large)
    public GameObject objectInScene;

    protected float lowValDeviation = 0.5f;
    protected float hightValDeviation = 1.2f;

    public void Move()
    {
        Move(directionMove);
    }
    public override void Destroy(int who = 0)
    {
        Debug.Log("Destroy: в астероид попали!");

    }

    public void InitializeRandomAsteroid()
    {
        SetStartRandomPosition();
        SetRandomRotation();
        SetRandomForceRotate();
        SetRandomDirection();
    }

    public void InitializeAsteroid(Vector3 position, Vector3 direction)
    {
        SetPosition(position);
        SetRandomRotation();
        SetRandomForceRotate();
        SetDirection(direction);
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
