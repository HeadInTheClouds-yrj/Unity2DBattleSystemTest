using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }
    public InputEvent InputEvent { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        Instance= this;
        InputEvent = new InputEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
