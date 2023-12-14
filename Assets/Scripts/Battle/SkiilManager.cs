using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiilManager : MonoBehaviour
{
    [SerializeField] private GameObject flySword;
    public static SkiilManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        EventManager.Instance.InputEvent.OnGetLeftMouseDown += SwordInstantiate;
    }
    private void OnDisable()
    {
        EventManager.Instance.InputEvent.OnGetLeftMouseDown -= SwordInstantiate;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
           
    }
    public void SwordInstantiate(Transform parent)
    {
        Object.Instantiate(flySword,parent.position,Quaternion.Euler(new Vector3(0,0,180)));
    }
}
