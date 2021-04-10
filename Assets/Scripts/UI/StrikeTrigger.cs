using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StrikeTrigger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    GameControl gControl;

    private Vector2 position;
    private bool isPressed;
    public Vector2 Position { get { return position; } }
    public bool IsPressed { get { return isPressed; } }
    public void OnPointerDown(PointerEventData eventData)
    {
        position = eventData.position;
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        //GameObject go = GameObject.Find("GameController");
        //gControl = go.GetComponent<GameControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
