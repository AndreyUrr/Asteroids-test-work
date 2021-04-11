using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float kMoveSpeed = 1.2f;
    [SerializeField]
    public Vector3 directionMove;
    public float forceRotation;
    public float allowance = 40f;  //допуск за границу экрана


    public virtual void Move(Vector3 vector)
    {
        this.gameObject.transform.position += vector * kMoveSpeed * Time.deltaTime;
        Vector3 worldPos = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
        worldPos.z = 1;

        if (worldPos.x > Screen.width + allowance)
        {
            this.gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(worldPos.x - Screen.width - 2*allowance, worldPos.y, worldPos.z));
        }
        if (worldPos.x < 0 - allowance)
        {
            this.gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(worldPos.x + Screen.width + 2*allowance, worldPos.y, worldPos.z));
        }
        if (worldPos.y > Screen.height + allowance)
        {
            this.gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(worldPos.x, worldPos.y - Screen.height - 2*allowance, worldPos.z));
        }
        if (worldPos.y < 0 - allowance)
        {
            this.gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(worldPos.x, worldPos.y + Screen.height + 2*allowance, worldPos.z));
        }
    }
    public virtual void Rotate()
    {
        Rotate(forceRotation);
    }
    public virtual void Rotate(float rotation)
    {
        this.gameObject.transform.Rotate(0, 0, rotation * Time.deltaTime * 3);
    }
    public virtual void Destroy(int who = 0)
    {
        
    }

    public void SetDirection(Vector3 direction)
    {
        directionMove = direction;
    }
    protected virtual void SetRandomDirection()
    {
        directionMove = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0);
    }

    protected void SetRandomForceRotate()
    {
        forceRotation = Random.Range(-20f, 20f);
    }
    public void SetPosition(Vector3 position)
    {
        this.gameObject.transform.position = position;
    }
    protected virtual void SetStartRandomPosition()
    {
        int placeGenerate = Random.Range(1, 3);
        Vector3 screenPos;
        if (placeGenerate == 1)
            screenPos = new Vector3(-allowance, Random.Range(0f, Screen.height), 1);
        else
            screenPos = new Vector3(Random.Range(0f, Screen.width),-allowance, 1);

        this.gameObject.transform.position = Camera.main.ScreenToWorldPoint(screenPos);
    }
    protected virtual void SetRandomRotation()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 359f));
    }

    // Start is called before the first frame update
    void Start()
    {
        SetStartRandomPosition();
        SetRandomRotation();
        SetRandomForceRotate();
        SetRandomDirection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
