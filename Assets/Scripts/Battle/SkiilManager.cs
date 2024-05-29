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
        GameObject sword = Object.Instantiate<GameObject>(flySword,parent.position,Quaternion.Euler(new Vector3(0,0,180)));
        if (sword.transform.TryGetComponent<TrackingSword>(out TrackingSword trackingSword))
        {
            trackingSword.InitializedSword(transform,NpcManager.Instance.GetNpcs()[Random.Range(0,2)].transform,LayerMask.GetMask("Npc"),100f,4000f);
        }
    }
}
